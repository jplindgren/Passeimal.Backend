using Newtonsoft.Json;
using Passeimal.Backend.Siren.Converters;
using Passeimal.Backend.Siren.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace Passeimal.Backend.Siren.Formatting {
    public class SirenMediaTypeFormatter : JsonMediaTypeFormatter {
        public const string MediaType = "application/vnd.siren+json";
        private MediaTypeHeaderValue _mediaType;

        public SirenMediaTypeFormatter(string mediaType = "application/vnd.siren+json") {
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            SerializerSettings.ContractResolver = new SirenContractResolver();
            SerializerSettings.Converters.Add(new HrefJsonConverter());
            //SerializerSettings.Converters.Add(new FieldsJsonConverters());

            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(mediaType));
        }

        public override bool CanReadType(Type type) {
            return true;
        }

        public override bool CanWriteType(Type type) {
            return true;
        }
    }// class
}
