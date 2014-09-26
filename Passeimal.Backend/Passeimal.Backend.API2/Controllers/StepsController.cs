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
    [EnableCors("*","*","*")]
    public class StepsController : ApiController {

        private IDataContext _dataContext;
        private StepRepository _repository;

        public StepsController (){
            _dataContext = new DataContext();
            _repository = new StepRepository(_dataContext);
        }

        // GET api/Step/5
        public HttpResponseMessage GetStep(Guid id) {
            Step step = _repository.Get(id);
            if (step == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, step);
        }

        // POST api/Step
        [ResponseType(typeof(Step))]
        public HttpResponseMessage PostStep(Step step) {
            step.Id = Guid.NewGuid();
            step.HappenedAt = DateTime.Now;
            if (!ModelState.IsValid) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _repository.Add(step);

            try {
                _repository.SaveChanges();
            } catch (DbUpdateException) {
                if (StepExists(step.Id)) {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                } else {
                    throw;
                }
            }
            return Request.CreateResponse(HttpStatusCode.Created, step);
        }

        // DELETE api/Step/5
        public HttpResponseMessage DeleteStep(Guid id) {
            Step step = _repository.Get(id);
            if (step == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _repository.Remove(step);
            _repository.SaveChanges();
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
