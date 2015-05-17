using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Domain.Geometry {
    public class BoundingBox {
        public BoundingBox(Location minPoint, Location maxPoint) {
            this.MinPoint = minPoint;
            this.MaxPoint = maxPoint;
        }
        public Location MinPoint { get; private set; }
        public Location MaxPoint { get; private set; }
    } //class
}
