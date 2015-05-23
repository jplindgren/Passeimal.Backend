using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Passeimal.Backend.API2.Controllers {
    public class StaticController : ApiController {
        // GET api/static/imageName
        public HttpResponseMessage Get(string id) {
            string imageName;
            if (id == "warning") {
                imageName = "restaurant-icon64Warning.png";
            } else if (id == "danger") {
                imageName = "restaurant-icon64Danger.png";
            }else{
                imageName=  "restaurant-icon64Green.png";
            }
            var response = new HttpResponseMessage();
            response.Content = new StreamContent(File.Open(AppDomain.CurrentDomain.BaseDirectory + "Images\\" + imageName, FileMode.Open));
            return response;
        }

        public HttpResponseMessage Get() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
   
    } //class
}