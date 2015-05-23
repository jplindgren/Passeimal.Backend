using Passeimal.Backend.Domain;
using Passeimal.Backend.Siren.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Passeimal.Backend.ConsoleClient {
    class Program {
        private static HttpClient _client;
        static private MediaTypeFormatterCollection _formatters = new MediaTypeFormatterCollection();
        static void Main(string[] args) {
            ConfigureClient();

            var test = GetStepsAsync();
            Console.WriteLine(test);
            Console.Read();
        }

        private static void ConfigureClient() {

            _client = new HttpClient() {
                BaseAddress = new Uri("http://localhost:3641")
            };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.siren+json"));
            _formatters.Add(new SirenMediaTypeFormatter());
        }

        public static Task<string> GetStringAsync(string value = "steps", object values = null) {
            var uri = (null == values) ? string.Format("/api/{0}", value) : string.Format("/api/{0}?{1}", value, values.ToString());

            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri, UriKind.Relative)
            };

            return _client.SendAsync(request)
                .ContinueWith(x => x.Result.Content.ReadAsStringAsync())
                .Unwrap();
        }

        public static object GetStepsAsync(object values = null) {
            var uri = (null == values) ? "/api/steps" : string.Format("{0}?{1}", "/api/steps", values.ToString());

            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri, UriKind.Relative)
            };

            return _client.SendAsync(request)
                .ContinueWith(x => x.Result.Content.ReadAsAsync<Step>(_formatters))
                .Unwrap().Result;
        }
    }// class
}
