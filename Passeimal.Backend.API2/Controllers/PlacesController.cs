using Passeimal.Backend.API2.Filters;
using Passeimal.Backend.API2.Models;
using Passeimal.Backend.Dal;
using Passeimal.Backend.Dal.Repositories;
using Passeimal.Backend.Domain;
using Passeimal.Backend.Domain.Geometry;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Passeimal.Backend.API2.Controllers {
    [AllowCrossSiteJson]   
    public class PlacesController : ApiController {
        private IDataContext _dataContext;
        private GeometryService geometryService;
        private PlaceQuery placeQuery;
        private int _pageSize = 20;
        public PlacesController() {
            _dataContext = new DataContext();
            geometryService = new GeometryService();
            placeQuery = new PlaceQuery(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        // GET api/Step/1
        [HttpGet]
        public HttpResponseMessage Get(double latitude, double longitude, int index = 0) {
            var boundingBox = geometryService.GetBoundingBox(new Location(latitude, longitude), 2);

            var clock = new Stopwatch();
            clock.Start();
            var average = placeQuery.GetAvegarePukesPoints();
            clock.Stop();
            Trace.WriteLine("********Tempo AVG: " + clock.ElapsedMilliseconds);

            clock.Reset();
            clock.Start();
            var placeList = placeQuery.GetByBoundingBox(_pageSize * index, _pageSize, boundingBox)
                            .Select(x => new PlaceRepresentation(average) {
                                    Id = x.Id,
                                    Latitude = x.Location.Latitude,
                                    Longitude = x.Location.Longitude,
                                    Name = x.Name,
                                    Vicinity = x.Vicinity,
                                    PukePoints = x.GetPukePoints()
                                });

            var result = new CollectionRepresentation<PlaceRepresentation>(placeList.ToList(), "api/Step/0", "api/Step/2");

            clock.Stop();
            Trace.WriteLine("********Tempo Places Found: " + clock.ElapsedMilliseconds);

            return Request.CreateResponse(HttpStatusCode.OK, placeList);
        }

        // POST api/place
        [ResponseType(typeof(Place))]
        public HttpResponseMessage Post(PlaceRepresentation place) {
            if (!ModelState.IsValid) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var placeEntity = new Place() {
                FormattedAddress = place.FormattedAddress,
                GooglePlaceId = place.GooglePlaceId,
                Icon = "http://maps.gstatic.com/mapfiles/place_api/icons/restaurant-71.png",
                Location = new Location(place.Latitude, place.Longitude),
                Name = place.Name,
                Vicinity = place.Vicinity
            };            

            try {
                _dataContext.Set<Place>().Add(placeEntity);
                _dataContext.SaveChanges();
            } catch (DbUpdateException) {
                if (PlaceExists(placeEntity.Id)) {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                } else {
                    throw;
                }
            }
            return Request.CreateResponse(HttpStatusCode.Created, place);
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [ResponseType(typeof(Place))]
        [HttpPut]
        public HttpResponseMessage Put([FromBody]PukeMessage puckMessage) {
            var place = _dataContext.Set<Place>().Where(x => x.Id == puckMessage.PlaceId).FirstOrDefault();
            var puck = new Puke((Severity)puckMessage.PukeVal, string.Empty);
            place.Pukes.Add(puck);

            try {
                _dataContext.SaveChanges();
            } catch (DbUpdateException) {
                throw;
            }
            return Request.CreateResponse(HttpStatusCode.Created, puck);
        }

        // DELETE api/place/5
        public HttpResponseMessage Delete(int id) {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private bool PlaceExists(Guid guid) {
            return false;
        }

        public class PukeMessage {
            public Guid PlaceId { get; set; }
            public int PukeVal { get; set; }
        }

    }//class
}