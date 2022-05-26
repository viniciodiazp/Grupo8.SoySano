using System;
using System.Collections.Generic;
using System.Text;

namespace Grupo8.SoySano.Utils
{
    public class Constant
    {
        public class Services
        {
            private static readonly string SERVICE_BASE_URL_LOCAL = "192.168.100.37:8080";
            private static readonly string SERVICE_BASE_URL_AWS = "soysanoapi-env.eba-ema8qtnj.us-east-1.elasticbeanstalk.com";
            public static readonly string SERVICE_BASE_URL = string.Format("http://{0}/api", SERVICE_BASE_URL_AWS);
            public static readonly string SERVICE_S3_BASE_URL = "https://s3.amazonaws.com/uisrael.grupo8";
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
