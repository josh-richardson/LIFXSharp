using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace rchrdsn.LIFX.LifxObjects {
    public class LifxLocation : LifxObject {
  
        public string name { get; set; }
          public LifxLocation(string json, LifxManager manager) {
              this.manager = manager;
              var bulbData = JToken.Parse(json);
            this.name = bulbData.Value<string>("name");
            this.id = bulbData.Value<string>("id");
        }
    }
}
