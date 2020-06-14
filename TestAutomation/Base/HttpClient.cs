using NUnit.Framework;
using RestSharp;
using System;

namespace TestAutomation.Base
{
    public class HttpClient
    {
        public string ApiMethodPath { get; set; }
        public IRestResponse Response { get; set; }
        public IRestRequest Request { get; set; }
        public RestClient RestClient { get; set; } = new RestClient();
    }
}
