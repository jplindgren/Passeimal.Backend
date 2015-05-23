using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Domain.Geometry {
    [ComplexType]
    public class Location {
        public Location() { }
        public Location(double latitude, double longitude) {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        [Column("Latitude")]
        public double Latitude { get; private set; }

        [Column("Longitude")]
        public double Longitude { get; private set; }

    } //class
}
