using System.Collections.Generic;

namespace Passeimal.Backend.Siren.Models {
    public class Entities : List<Entity> {
        public Entities() {
        }

        public Entities(IEnumerable<Entity> collection)
            : base(collection) {
        }
    }
}
