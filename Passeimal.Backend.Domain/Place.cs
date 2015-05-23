using Passeimal.Backend.Domain.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Domain {
    public class Place {
        private int pukePoints;
        public Place() { }
        public Place(int pukePoints) {
            this.pukePoints = pukePoints;
        }
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string GooglePlaceId { get; set; }
        public Location Location { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Vicinity { get; set; }
        public string FormattedAddress { get; set; }
        public int Relevance { get; set; }

        public int GetPukePoints() {
            return pukePoints;
        }

        public virtual ICollection<Puke> Pukes { get; set; }
    } //class
}
