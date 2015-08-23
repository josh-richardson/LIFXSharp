using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace rchrdsn.LIFX.LifxObjects {
    public class LifxCapability {
        public bool hasColor { get; set; }
        public bool hasVariableColorTemp { get; set; }
          public LifxCapability(string json) {
              JToken bulbData = JToken.Parse(json);
            this.hasColor= bulbData.Value<bool>("has_color");
            this.hasVariableColorTemp = bulbData.Value<bool>("has_variable_color_temp");
        }
    }
}
