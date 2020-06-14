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
        public void WhenTheSampleRequestIsPostedToApi()
        {
            //TODO: async?
            _httpClient.Response = _httpClient.RestClient.ExecuteAsPost(_httpClient.Request, "POST");
        }

        [Given(@"the sample request with a valid JWT token")]
        public void GivenTheSampleRequestWithAValidJWTToken()
        {
            _httpClient.Request.AddHeader(Headers.X_Auth_Key.AsString(EnumFormat.Description), ConfigSettings.ValidToken);
        }



        [Then(@"Api returns ""(.*)"" name as ""(.*)""")]
        public void ApiReturns(string key, string value)
        {
            Assert.That(_httpClient.Response.GetResponseObject(key), Is.EqualTo(value), $"The {key} is not matching");
        }
    }
}
