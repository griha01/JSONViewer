using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONViewerProject.Model
{
    class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Context Context { get; set; }
        public string Level { get; set; }

    }
    class Context
    {
        [JsonProperty("Масса, кг")]
        public int? Mass { get; set; }

        [JsonProperty("Объём, м3")]
        public string Volume { get; set; }

        public string C3d { get; set; }

    }
}
