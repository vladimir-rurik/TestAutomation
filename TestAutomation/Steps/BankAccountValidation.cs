
using EnumsNET;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestAutomation.Base;
using TestAutomation.Model;
using TestAutomation.Utilities;
using static TestAutomation.Base.Enumerations;

namespace TestAutomation.Steps
{
    [Binding]
    class BankAccountValidation
    {
        private readonly HttpClient _httpClient;

        public BankAccountValidation(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }

        [Given(@"the sample request without a JWT token and ""(.*)""")]
        public void SampleRequestWithoutJWTtoken(string bankAccount)
        {
            _httpClient.Request.AddJsonBody(new BankAccountDTO() { BankAccount = bankAccount });
        }

        [Given(@"the sample request with an empty JWT token and ""(.*)""")]
        public void SampleRequestWithEmptyJWTtoken(string bankAccount)
        {
            _httpClient.Request.AddHeader(Headers.X_Auth_Key.AsString(EnumFormat.Description), "");

            _httpClient.Request.AddJsonBody(new BankAccountDTO() { BankAccount = bankAccount });
        }

        [When(@"the sample request is posted to api")]
        public async Task WhenTheSampleRequestIsPostedToApiAsync()
        {
            _httpClient.Response = await _httpClient.RestClient.ExecuteAsync(_httpClient.Request);
        }

        [Given(@"the sample request with a valid JWT token and ""(.*)""")]
        public void GivenTheSampleRequestWithAValidJWTToken(string bankAccount)
        {
            _httpClient.Request.AddHeader(Headers.X_Auth_Key.AsString(EnumFormat.Description), ConfigSettings.ValidToken);

            _httpClient.Request.AddJsonBody(new BankAccountDTO() { BankAccount = bankAccount });
        }

        [Then(@"Api returns ""(.*)"" name as ""(.*)""")]
        public void ApiReturns(string key, string value)
        {
            if (key.First() == '*') //Regex
            {
                key = key.Substring(1);
                var responseValue = _httpClient.Response.GetResponseContentObject(key);

                Assert.That(responseValue, Does.Match(value), $"The content key:{key} does not match.");
            }
            else
            {
                var responseValue = _httpClient.Response.GetResponseContentObject(key);

                Assert.That(responseValue, Is.EqualTo(value), $"The content key:{key} does not match.");
            }
        }

        [Then(@"Api returns StatusCode name as ""(.*)""")]
        public void ThenApiReturnsStatusCodeNameAs(int value)
        {
            int statusCode = (int) _httpClient.Response.StatusCode;
            Assert.That(statusCode, Is.EqualTo(value));

        }

    }
}
