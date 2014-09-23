using Newtonsoft.Json;
using Passeimal.Backend.Siren.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Siren.Converters {
    public class HrefJsonConverter : JsonConverter{
        public override bool CanConvert(Type objectType) {
            return object.Equals(objectType, typeof(Href));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            switch (reader.TokenType) {
                case JsonToken.String:
                    return new Href((string)reader.Value);
                case JsonToken.Null:
                    return null;
                default:
                    throw new InvalidOperationException(string.Format("Unable to deserialize Href from token type {0}",reader.TokenType));
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (null == value && serializer.NullValueHandling == NullValueHandling.Include) {
                writer.WriteNull();
                return;
            }

            var href = value as Href;
            if (href != null) {
                writer.WriteValue(href.OriginalString);
                return;
            }

            throw new InvalidOperationException(string.Format("Unable to serialize {0} with {1}", value.GetType(), typeof(HrefJsonConverter).Name));
        }
    }// class
}
