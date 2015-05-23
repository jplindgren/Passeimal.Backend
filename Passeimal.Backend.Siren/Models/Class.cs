using System.Collections.Generic;

namespace Passeimal.Backend.Siren.Models {
    public class Class : List<string> {
        public static implicit operator Class(string value) {
            return new Class { value };
        }
    }
}
