using AventStack.ExtentReports;
using AventStack.ExtentReports.Configuration;
using AventStack.ExtentReports.Reporter;
using EnumsNET;
using Microsoft.Extensions.Configuration;
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
using static TestAutomation.Base.Enumerations;

namespace TestAutomation.Steps
{
    [Binding]
    class BankAccountValidation
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public BankAccountValidation(HttpClient httpClient, IConfiguration config)
        { 
            _httpClient = httpClient;
            _config = config;
        }

        [Given(@"the sample request without a JWT token")]
        public void SampleRequestWithoutJWTtoken()
        {
            BankAccountDTO bankAccountDTO = new BankAccountDTO()
            {
                BankAccount = "GB09HAOE91311808002317"
            };
            _httpClient.Request.AddJsonBody(bankAccountDTO);
        }

        [Given(@"the sample request with an empty JWT token and ""(.*)""")]
        public void SampleRequestWithEmptyJWTtoken(string bankAccount)
        {
            _httpClient.Request.AddHeader(Headers.X_Auth_Key.AsString(EnumFormat.Description), "");

            _httpClient.Request.AddJsonBody(new BankAccountDTO());
        }

        [When(@"the sample request is posted to api")]
        public void WhenTheSampleRequestIsPostedToApi()
        {
            //TODO: async?
            _httpClient.Response = _httpClient.RestClient.ExecuteAsPost(_httpClient.Request, "POST");
        }

        [Given(@"the sample request with a valid JWT token")]
        public void GivenTheSampleRequestWithAValidJWTToken()
        {
            _httpClient.Request.AddHeader(Headers.X_Auth_Key.AsString(EnumFormat.Description), _config["validToken"]);
        }



        [Then(@"Api returns ""(.*)"" name as ""(.*)""")]
        public void ApiReturns(string key, string value)
        {
            Assert.That(_httpClient.Response.GetResponseObject(key), Is.EqualTo(value), $"The {key} is not matching");
        }
    }
}
