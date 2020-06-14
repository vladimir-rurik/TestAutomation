using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using MongoDB.Bson;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TestAutomation.Base;
using TestAutomation.Model;
using TestAutomation.Utilities;

namespace TestAutomation.Steps
{
    [Binding]
    class ValidateBankAccount
    {
        private readonly Settings _settings;

        public ValidateBankAccount(Settings settings)
        { 
            _settings = settings;
        }

        [Given(@"a sample request without a JWT token")]
        public void SampleRequestWithoutJWTtoken()
        {
            _settings.Request = new RestRequest(_settings.ApiMethodPath, Method.POST);
            BankAccountDTO bankAccountDTO = new BankAccountDTO()
            {
                BankAccount = "GB09HAOE91311808002317"
            };
            _settings.Request.AddJsonBody(bankAccountDTO);
        }

        [When(@"the sample request is posted to api")]
        public void WhenTheSampleRequestIsPostedToApi()
        {
            //TODO: async?
            _settings.Response = _settings.RestClient.ExecuteAsPost(_settings.Request, "POST");
        }

        [Then(@"Api returns ""(.*)"" name as ""(.*)""")]
        public void ApiReturns(string key, string value)
        {
            Assert.That(_settings.Response.GetResponseObject(key), Is.EqualTo(value), $"The {key} is not matching");
        }
    }
}
