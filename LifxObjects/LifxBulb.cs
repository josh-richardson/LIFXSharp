using System;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace rchrdsn.LIFX.LifxObjects {
    public class LifxBulb : LifxObject {
        public string uuid { get; set; }
        public string label { get; set; }
        public bool connected { get; set; }
        public string power { get; set; }
        public LifxColor color { get; set; }
        public float brightness { get; set; }
        public LifxGroup group { get; set; }
        public LifxLocation location { get; set; }
        public string productName { get; set; }
        public LifxCapability capabilities { get; set; }
        public string lastSeen { get; set; }
        public double secondsSinceSeen { get; set; }

        public LifxBulb(string json, LifxManager manager) {
       
                JToken bulbData = JObject.Parse(json);
                this.manager = manager;
                id = bulbData.Value<string>("id");
                uuid = bulbData.Value<string>("uuid");
                label = bulbData.Value<string>("label");
                connected = bulbData.Value<bool>("connected");
                power = bulbData.Value<string>("power");
                brightness = bulbData.Value<float>("brightness");
                color = new LifxColor(bulbData.Value<JToken>("color").ToString());
                group = new LifxGroup(bulbData.Value<JToken>("group").ToString(), manager);
                location = new LifxLocation(bulbData.Value<JToken>("location").ToString(), manager);
                productName = bulbData.Value<string>("product_name");
                capabilities = new LifxCapability(bulbData.Value<JToken>("capabilities").ToString());
                lastSeen = bulbData.Value<string>("last_seen");
                secondsSinceSeen = bulbData.Value<double>("seconds_since_seen");
        
            
        }
    }
}
