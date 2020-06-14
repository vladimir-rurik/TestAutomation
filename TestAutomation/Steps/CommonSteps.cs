using EnumsNET;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using TestAutomation.Base;
using static TestAutomation.Base.Enumerations;

namespace TestAutomation.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly HttpClient _httpClient;

        public CommonSteps(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Given(@"a sample request with a json content type")]
        public void GivenASsmpleRequestWithAJsonContentType()
        {
            _httpClient.Request = new RestRequest(_httpClient.ApiMethodPath, Method.POST);
            _httpClient.Request.AddHeader(Headers.ContentType.AsString(EnumFormat.Description), "application/json");
        }
    }
}

