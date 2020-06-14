using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestAutomation.Utilities
{
    public static class Extensions
    {

        public static string GetResponseContentObject(this IRestResponse response, string responseObject)
        {
            JObject obj = JObject.Parse(response.Content.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }));

            obj[responseObject] ??= "";
            return obj[responseObject].ToString();
        }

    }
}
