using System;
using System.Net;
using System.IO;

namespace Grupo8.SoySano.Utils
{
    public class RestExecutor
    {
        private readonly WebClient client = new WebClient();

        public String Execute(string resourceUri, string method, string payload = null)
        {
            string response = null;
            try
            {
                Console.WriteLine("Executing: " + resourceUri);
                switch (method)
                {
                    case "POST":
                    case "PUT":
                        client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                        client.Headers.Add("Accept", "application/json");
                        response = client.UploadString(resourceUri, method, payload);
                        break;
                    case "GET":
                        response = ExecGet(resourceUri);
                        break;
                    case "DELETE":
                        response = ExecDelete(resourceUri);
                        break;
                    default:
                        throw new NotImplementedException(string.Format("Method {0} is not implemented", method));
                }

                Console.WriteLine("RESULT:-------------");
                Console.WriteLine(response);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
            return response;
        }

        private string ExecGet(string resourceUri)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(resourceUri);
            httpRequest.Method = "GET";
            httpRequest.Accept = "application/json";

            string result = null;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            Console.WriteLine(httpResponse.StatusCode);
            return result;
        }

        private string ExecDelete(string resourceUri)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(resourceUri);
            httpRequest.Method = "DELETE";

            httpRequest.Accept = "application/json";

            string result = null;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            Console.WriteLine(httpResponse.StatusCode);
            return result;
        }
    }
}
