using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Domain{
    public class Step{
        private Step() { }
        public Step(string description) {
            Id = Guid.NewGuid();
            HappenedAt = DateTime.Now;
            Description = description;
        }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime HappenedAt { get; set; }
    }// class
}
