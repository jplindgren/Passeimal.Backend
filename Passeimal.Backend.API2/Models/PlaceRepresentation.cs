using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passeimal.Backend.API2.Models {
    public class PlaceRepresentation {
        private int average;

        public PlaceRepresentation(int averagePukePoints) {
            this.average = averagePukePoints;
        }
        public Guid Id { get; set; }

        public string GooglePlaceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Icon { 
            get {
                if (this.average == 0)
                    return "http://maps.gstatic.com/mapfiles/place_api/icons/restaurant-71.png";
                else if (PukePoints == 0 || PukePoints <= (this.average / 2))
                    return "http://localhost:38383/images/restaurant-icon64Green.png";
                else if (PukePoints > (this.average / 2) && PukePoints < (this.average * 2))
                    return "http://localhost:38383/images/restaurant-icon64Warning.png";
                else if (PukePoints >= (this.average * 2))
                    return "http://localhost:38383/images/restaurant-icon64Danger.png";
                return "http://maps.gstatic.com/mapfiles/place_api/icons/restaurant-71.png";
            }
        }
        public string Name { get; set; }
        public string Vicinity { get; set; }
        public string FormattedAddress { get; set; }
        public int PukePoints { get; set; }
    } //class
}