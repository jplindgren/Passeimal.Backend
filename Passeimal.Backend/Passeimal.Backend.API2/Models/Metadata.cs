using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Passeimal.Backend.API2.Models {
    public class CollectionMetadata<T> where T : class {
        public int TotalResults { get; set; }
        public int ReturnedResults { get; set; }
        public T[] Results { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }

        public CollectionMetadata(HttpResponseMessage httpResponse) {
            this.Timestamp = DateTime.Now;
            if (httpResponse.Content != null && httpResponse.IsSuccessStatusCode) {
                this.TotalResults = 1;
                this.ReturnedResults = 1;
                this.Status = "Success";

                IEnumerable<T> enumResponseObject;
                httpResponse.TryGetContentValue<IEnumerable<T>>(out enumResponseObject);
                this.Results = enumResponseObject.ToArray();
                this.ReturnedResults = enumResponseObject.Count();
                
            } else {
                this.Status = "Error";
                this.ReturnedResults = 0;
            }
        }
    } //class
}