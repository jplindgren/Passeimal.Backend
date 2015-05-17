using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Domain.Geometry {
    public class Location {
        public Location() { }
        public Location(double latitude, double longitude) {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

    } //class
}
