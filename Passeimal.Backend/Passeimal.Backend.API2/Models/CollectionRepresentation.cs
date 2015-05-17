using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passeimal.Backend.API2.Models {
    public class CollectionRepresentation<T> {
        IEnumerable<T> Result { get; set; }

        string Next { get; set; }
        string Previous { get; set; }

        public CollectionRepresentation(IEnumerable<T> collection, string next, string previous) {
            this.Result = collection;
            this.Next = next;
            this.Previous = previous;
        }        
    } //class
}