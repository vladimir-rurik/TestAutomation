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
        private readonly HttpClient _httpClient;

        public ValidateBankAccount(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }

        [Given(@"a sample request without a JWT token")]
        public void SampleRequestWithoutJWTtoken()
        {
            _httpClient.Request = new RestRequest(_httpClient.ApiMethodPath, Method.POST);
            _httpClient.Request.AddHeader("Content-Type", "application/json");

            BankAccountDTO bankAccountDTO = new BankAccountDTO()
            {
                BankAccount = "GB09HAOE91311808002317"
            };
            _httpClient.Request.AddJsonBody(bankAccountDTO);
        }

        [Given(@"a sample request with an empty JWT token")]
        public void SampleRequestWithEmptyJWTtoken()
        {
            _httpClient.Request = new RestRequest(_httpClient.ApiMethodPath, Method.POST);
            _httpClient.Request.AddHeader("Content-Type", "application/json");
            _httpClient.Request.AddHeader("X-Auth-Key", "");

            BankAccountDTO bankAccountDTO = new BankAccountDTO()
            {
                BankAccount = "GB09HAOE91311808002317"
            };
            _httpClient.Request.AddJsonBody(bankAccountDTO);
        }

        [When(@"the sample request is posted to api")]
        public void WhenTheSampleRequestIsPostedToApi()
        {
            //TODO: async?
            _httpClient.Response = _httpClient.RestClient.ExecuteAsPost(_httpClient.Request, "POST");
        }

        [Then(@"Api returns ""(.*)"" name as ""(.*)""")]
        public void ApiReturns(string key, string value)
        {
            Assert.That(_httpClient.Response.GetResponseObject(key), Is.EqualTo(value), $"The {key} is not matching");
        }
    }
}
