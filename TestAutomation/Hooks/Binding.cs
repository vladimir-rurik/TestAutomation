
using TestAutomation.Base;
using System;
using System.Configuration;
using System.IO;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MongoDB.Bson;

namespace TestAutomation.Hooks
{
    [Binding]
    public class Binding
    {
        //Global Variable for Extend report
        private static ExtentTest _featureName;
        private static AventStack.ExtentReports.ExtentReports _extent;

        private ExtentTest _scenario;
        private Settings _settings;
        private ScenarioContext _scenarioContext;


        public Binding(Settings settings, ScenarioContext scenarioContext)
        {
            _settings = settings;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void TestSetup()
        {
            _settings.BaseUrl = new Uri("https://api-test.afterpay.dev/");
            _settings.RestClient.BaseUrl = _settings.BaseUrl;

            _settings.ApiMethodPath = "api/v3/validate/bank-account";
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            string file = "ExtentReport.html";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);

            //Initialize an Extent report before starting the test
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            //Attach the report to the reporter
            _extent = new AventStack.ExtentReports.ExtentReports();
            _extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush the report once the test is completed
            _extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create a dynamic feature name
            _featureName = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {

            string stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                switch (stepType)
                {
                    case nameof(Given):
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case nameof(When):
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                        _scenario.CreateNode<When>($"<pre>{LogRequest(_settings.Request)}</pre>");
                        _scenario.CreateNode<When>($"<pre>{LogResponse(_settings.Response)}</pre>");

                        break;
                    case nameof(Then):
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case nameof(And):
                        _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                }
            }
            else if (_scenarioContext.TestError != null)
            {
                switch (stepType)
                {
                    case nameof(Given):
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                        break;
                    case nameof(When):
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                        break;
                    case nameof(Then):
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        break;
                }
            }
        }

        [BeforeScenario]
        public void Initialize()
        {
            //Create a dynamic scenario name
            _scenario = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        private string LogRequest(IRestRequest request)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                // otherwise it will just show the enum value
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),
                // ToString() here to have the method as a nice string otherwise it will just show the enum value
                method = request.Method.ToString(),
                //// This will generate the actual Uri used in the request
                //uri = _restClient.BuildUri(request),
            };

            string json = JsonConvert.SerializeObject(new { Request = requestToLog });
            return JValue.Parse(json).ToString(Formatting.Indented);
        }

        private string LogResponse(IRestResponse response)
        {
            var responseToLog = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers,
                // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage,
            };

            string json = JsonConvert.SerializeObject(new { Response = responseToLog });

            return JValue.Parse(json).ToString(Formatting.Indented);

        }

    }

}
