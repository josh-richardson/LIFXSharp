using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace rchrdsn.LIFX.LifxObjects {
    public class LifxColor {
        private float hue { get; set; }
        private int kelvin { get; set; }
        private string colorString { get; set; }

        public LifxColor(string json) {
            JToken colorData = JToken.Parse(json);
            this.hue = colorData.Value<float>("hue");
            this.kelvin = colorData.Value<int>("kelvin");
            if (colorData.Value<float>("saturation") != 0) {
                this.colorString = "hue:" + hue;
            } else {
                this.colorString = "kelvin:" + kelvin;
            }
        }


        public LifxColor(int kelvin) {
            this.kelvin = kelvin;
            colorString = "kelvin:" + kelvin;
        }

        public LifxColor(Color color) {
            this.hue = color.GetHue();
            this.colorString = "hue:" + hue;
        }

        public Color getSystemColor() {
            return colorString.Contains("kelvin") ? colorFromKelvin(kelvin) : colorFromHSV(hue, 1, 1);
        }

        public string getLifxColorString() {
            return colorString;
        }

        public bool isUsingKelvin() {
            return colorString.Contains("kelvin");
        }


        private static Color colorFromHSV(double hue, double saturation, double value) {
            var hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            var f = hue / 60 - Math.Floor(hue / 60);
            value = value * 255;
            var v = Convert.ToInt32(value);
            var p = Convert.ToInt32(value * (1 - saturation));
            var q = Convert.ToInt32(value * (1 - f * saturation));
            var t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }



        private static Color colorFromKelvin(long tmpKelvin) {
            long r, g, b;
            double tmpCalc = 0;
            if (tmpKelvin < 1000)
                tmpKelvin = 1000;
            if (tmpKelvin > 40000)
                tmpKelvin = 40000;
            tmpKelvin = tmpKelvin / 100;
            if (tmpKelvin <= 66) {
                r = 255;
            } else {
                tmpCalc = tmpKelvin - 60;
                tmpCalc = 329.698727446 * (Math.Pow(tmpCalc, -0.1332047592));
                r = (long)tmpCalc;
                if (r < 0)
                    r = 0;
                if (r > 255)
                    r = 255;
            }

            if (tmpKelvin <= 66) {
                tmpCalc = tmpKelvin;
                tmpCalc = 99.4708025861 * Math.Log(tmpCalc) - 161.1195681661;
                g = (long)tmpCalc;
                if (g < 0)
                    g = 0;
                if (g > 255)
                    g = 255;
            } else {
                tmpCalc = tmpKelvin - 60;
                tmpCalc = 288.1221695283 * (Math.Pow(tmpCalc, -0.0755148492));
                g = (long)tmpCalc;
                if (g < 0)
                    g = 0;
                if (g > 255)
                    g = 255;
            }
            if (tmpKelvin >= 66) {
                b = 255;
            } else if (tmpKelvin <= 19) {
                b = 0;
            } else {
                tmpCalc = tmpKelvin - 10;
                tmpCalc = 138.5177312231 * Math.Log(tmpCalc) - 305.0447927307;
                b = (long)tmpCalc;
                if (b < 0)
                    b = 0;
                if (b > 255)
                    b = 255;
            }
            return Color.FromArgb((int)r, (int)g, (int)b);
        }

    }
}
