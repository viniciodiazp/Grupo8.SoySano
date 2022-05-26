using System;
using System.Collections.Generic;
using System.Text;

namespace Grupo8.SoySano.Utils
{
    public class Constant
    {
        public class Services
        {
            public static readonly string SERVICE_BASE_URL = "http://192.168.100.37:8080/api";
        }

        public class Method
        {
            public static readonly string GET = "GET";
            public static readonly string POST = "POST";
            public static readonly string PUT = "PUT";
            public static readonly string DELETE = "DELETE";
        }

        public class Messages
        {
            public static readonly string DISPLAY_TITLE = "Soy Sano";
        }
    }
}
