using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TestAutomation.Base;

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
        public void GivenSimpleRequestWithJsonContentType()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
