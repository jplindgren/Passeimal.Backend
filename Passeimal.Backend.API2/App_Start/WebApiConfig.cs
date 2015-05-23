using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Passeimal.Backend.API2.Filters;
using Passeimal.Backend.Siren.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;

namespace Passeimal.Backend.API2 {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.Routes.MapHttpRoute("FilesRoute", "images/{*pathInfo}", null, null, new StopRoutingHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
            //config.Formatters.Add(new SirenMediaTypeFormatter());

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            var serverCompressionHandler = new ServerCompressionHandler(4096, new GZipCompressor(), new DeflateCompressor());
            config.MessageHandlers.Insert(0, serverCompressionHandler);
            config.MessageHandlers.Add(new CollectionMetadataHandler());

            config.Formatters.RemoveAt(1);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
        }
    }
}
