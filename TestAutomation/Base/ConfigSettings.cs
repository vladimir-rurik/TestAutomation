using AventStack.ExtentReports.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestAutomation.Base
{
    public class ConfigSettings
    {
        private static IConfiguration _config;

        public Uri BaseUrl { get; set; }
        public string ApiMethodPath { get; set; }
        public string ValidToken { get; set; }

        public ConfigSettings()
        {
            if (_config == null)
            {
                _config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.test.json")
                    .Build();

                BaseUrl = new Uri(_config["baseUrl"]);
                ApiMethodPath = _config["apiMethodPath"];
                ValidToken = _config["validToken"];
            }
            
        }
    }

}
