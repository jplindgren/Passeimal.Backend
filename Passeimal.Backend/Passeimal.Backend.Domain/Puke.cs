using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Domain {
    public class Puke {
        public Puke() {}
        public Puke(Severity severity, string note) {
            CreatedAt = DateTime.Now;
            Severity = severity;
            Note = note;
        }

        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public Severity Severity { get; set; }
        public string Note { get; set; }      
    } //class
}
