using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passeimal.Backend.API2.Models {
    public class PlaceRepresentation {
        public Guid Id { get; set; }

        public string GooglePlaceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Vicinity { get; set; }
        public string FormattedAddress { get; set; }
    } //class
}