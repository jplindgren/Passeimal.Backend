using Passeimal.Backend.API2.Filters;
using Passeimal.Backend.Dal;
using Passeimal.Backend.Dal.Repositories;
using Passeimal.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace Passeimal.Backend.API2.Controllers{
    [AllowCrossSiteJson]    
    public class StepsController : ApiController {

        private IDataContext _dataContext;
        private StepRepository _repository;

        public StepsController (){
            _dataContext = new DataContext();
            _repository = new StepRepository(_dataContext);
        }

        // GET api/Step/5
        public HttpResponseMessage Get() {
            var stepsBatch = _dataContext.Set<Step>()
                .OrderByDescending(x => x.HappenedAt)
                .Skip(0)
                .Take(20)
                .ToList();
            return Request.CreateResponse(HttpStatusCode.OK, stepsBatch);
        }

        // GET api/Step/5
        public HttpResponseMessage Get(Guid id) {
            Step step = _repository.Get(id);
            if (step == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, step);
        }

        // POST api/Step
        [ResponseType(typeof(Step))]
        public HttpResponseMessage Post(Step step) {
            step.Id = Guid.NewGuid();
            step.HappenedAt = DateTime.Now;
            if (!ModelState.IsValid) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _dataContext.Set<Step>().Add(step);

            try {
                _dataContext.SaveChanges();
            } catch (DbUpdateException) {
                if (StepExists(step.Id)) {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                } else {
                    throw;
                }
            }
            return Request.CreateResponse(HttpStatusCode.Created, step);
        }

        // POST api/Step        
        [System.Web.Http.HttpOptions]
        public HttpResponseMessage Options() {            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/Step/5
        public HttpResponseMessage Delete(Guid id) {
            Step step = _repository.Get(id);
            if (step == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _dataContext.Set<Step>().Remove(step);
            _dataContext.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, step);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _dataContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StepExists(Guid id) {
            return _repository.GetCountById(id) > 0;
        }

    }// class
}
