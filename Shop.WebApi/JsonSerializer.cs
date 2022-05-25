using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebApi
{
    public class JsonSerializer
    {
        private readonly JsonSerializerSettings settings;

        public JsonSerializer()
        {
            this.settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public JsonSerializer(Formatting formatting)
        {
            this.settings = new JsonSerializerSettings
            {
                Formatting = formatting,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.Auto
            };
        }
        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, this.settings);
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
    }
}