using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using rchrdsn.LIFX.Properties;

namespace rchrdsn.LIFX.LifxObjects {
    public class LifxObject {
        public string id { get; set; }

        public LifxManager manager { get; set; }

        private string getSelector() {
            var selector = "id:" + id;
            if (this is LifxLocation) {
                selector = "location_" + selector;
            } else if (this is LifxGroup) {
                selector = "group_" + selector;
            }
            return selector;
        }

        public bool setColor(string color, int duration) {
            return manager.setColor(color, duration, getSelector());
        }

        public bool setColor(Color color, int duration) {
            return manager.setColor("rgb:" + color.R + "," + color.G + "," + color.B, duration, getSelector());
        }

        public bool setColor(LifxColor color, int duration) {
            return manager.setColor(color.getLifxColorString(), duration, getSelector());
        }


        public bool togglePower() {
            return manager.togglePower(getSelector());
        }

        public bool setPower(bool on) {
            return manager.setPower(on, getSelector());
        }
    }
}
