
using TestAutomation.Base;
using System;
using System.Configuration;
using System.IO;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;

namespace TestAutomation.Hooks
{
    [Binding]
    public class Binding
    {
        //Global Variable for Extend report
        private static ExtentTest _featureName;
        private static ExtentTest _scenario;
        private static AventStack.ExtentReports.ExtentReports _extent;

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
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);

            //Initialize Extent report before test starts
            var htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            //Attach report to reporter
            _extent = new AventStack.ExtentReports.ExtentReports();
            _extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
            _extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create dynamic feature name
            _featureName = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {

            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                switch (stepType)
                {
                    case nameof(Given):
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case nameof(When):
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
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
            //Create dynamic scenario name
            _scenario = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

    }
}
