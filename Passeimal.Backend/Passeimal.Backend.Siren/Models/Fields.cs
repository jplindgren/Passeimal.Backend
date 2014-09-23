using System.Collections.Generic;

namespace Passeimal.Backend.Siren.Models {
    public class Fields : List<Field> {
        public Fields() {
        }

        public Fields(IEnumerable<Field> collection)
            : base(collection) {
        }
    }
}
