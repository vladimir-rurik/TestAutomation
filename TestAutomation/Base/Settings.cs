using RestSharp;
using System;

namespace TestAutomation.Base
{
    public class Settings
    {
        public Uri BaseUrl { get; set; }
        public string ApiMethodPath { get; set; }
        public IRestResponse Response { get; set; }
        public IRestRequest Request { get; set; }
        public RestClient RestClient { get; set; } = new RestClient();
    }
}
