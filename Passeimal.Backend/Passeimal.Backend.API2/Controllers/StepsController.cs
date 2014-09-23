using Passeimal.Backend.Dal;
using Passeimal.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Passeimal.Backend.API2.Controllers{
    public class StepsController : ApiController {
        // GET api/steps/
        public IEnumerable<Step> Get() {
            using (var context = new DataContext()) {
                var list = context.Set<Step>().ToList();
                return list.AsEnumerable();
            }
        }

        // GET api/steps/5
        public Step Get(Guid id) {
            using (var context = new DataContext()) {
                return context.Set<Step>().Where(x => x.Id == id).FirstOrDefault();
            }
        }

        // POST api/values
        public void Post([FromBody]string value) {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/values/5
        public void Delete(int id) {
        }

    }// class
}
