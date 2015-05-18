using Passeimal.Backend.API2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Passeimal.Backend.API2.Filters {
    public class CollectionMetadataHandler : DelegatingHandler {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            return base.SendAsync(request, cancellationToken).ContinueWith(
                task => {
                    if (!ResponseIsValid(task.Result))
                        return task.Result;

                    object responseObject;
                    task.Result.TryGetContentValue(out responseObject);
                    if (responseObject is IEnumerable && !(responseObject is string)) {
                        ProcessObject<object>(responseObject as IEnumerable<object>, task.Result, true);
                    }
                    //} else {
                    //    var list = new List<object>();
                    //    list.Add(responseObject);
                    //    ProcessObject<object>(responseObject as IEnumerable<object>, task.Result, true);
                    //}
                    return task.Result;
             });
        }

        private void ProcessObject<T>(IEnumerable<T> responseObject, HttpResponseMessage response, bool isIQueryable) where T : class {
            var metadata = new CollectionMetadata<T>(response);
            //uncomment this to preserve content negotation, but remember about typecasting for DataContractSerliaizer
            //var formatter = GlobalConfiguration.Configuration.Formatters.First(t => t.SupportedMediaTypes.Contains(new MediaTypeHeaderValue(response.Content.Headers.ContentType.MediaType)));
            //response.Content = new ObjectContent<Metadata<T>>(metadata, formatter);
            response.Content = new ObjectContent<CollectionMetadata<T>>(metadata, GlobalConfiguration.Configuration.Formatters[0]);
        }

        private bool ResponseIsValid(HttpResponseMessage response) {
            if (response == null || (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created) || !(response.Content is ObjectContent)) 
                return false;
            return true;
        }
    } //class
}