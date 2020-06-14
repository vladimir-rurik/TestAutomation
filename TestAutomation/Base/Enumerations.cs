using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TestAutomation.Base
{
    public static class Enumerations
    {
        public enum Headers
        {
            [Description("X-Auth-Key")]
            X_Auth_Key,
            [Description("Content-Type")]
            ContentType
        }
    }
}
