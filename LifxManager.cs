using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using rchrdsn.LIFX.LifxObjects;
using rchrdsn.LIFX.Properties;

namespace rchrdsn.LIFX
{
    public class LifxManager : object {
        private readonly String _curlPath, _auth;

       public LifxManager(string curlPath, string auth){
            if (!System.IO.File.Exists(curlPath)) {
                throw new FileNotFoundException("cURL executable could not be found.");
            }
            if (auth.Length != 64) {
                throw new FormatException("Authentication token should be 64 characters long");
            }
            _auth = auth;
            _curlPath = curlPath;
        }



        public string getResponse(string arguments) {
            if (System.IO.File.Exists(_curlPath)) {
                
                try {
                    var curl = new Process {
                        StartInfo = {
                            FileName = _curlPath,
                            Arguments = arguments,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        },
                        EnableRaisingEvents = true
                    };

                    curl.Start();
                    return curl.StandardOutput.ReadToEnd();
                } catch (Exception ex) {
                    // ignored
                }

            }

            return "Failure";
       }


        public List<LifxBulb> listBulbs(string selector = "all") {
            var response = getResponse(Resources.listLights.Replace("%AUTH", _auth).Replace("%SELECTOR", selector));
            var array = JArray.Parse(response);
            return array.Select(str => new LifxBulb(str.ToString(), this)).ToList();
        }

        public bool setColor(string color, int duration, string selector = "all") {
            var request = Resources.setColor.Replace("%AUTH", _auth).Replace("%SELECTOR", selector).Replace("%COLOR", color).Replace("%DURATION", duration.ToString());
            var response = getResponse(request);
            return response.Contains(@"""ok");
        }

        public bool togglePower(string selector = "all") {
            var request = Resources.togglePower.Replace("%AUTH", _auth).Replace("%SELECTOR", selector);
            var response = getResponse(request);
            return response.Contains(@"""ok");
        }

        public bool setPower(bool on, string selector = "all") {
            var request = Resources.setPower.Replace("%AUTH", _auth).Replace("%STATE", (on ? "on" : "off")).Replace("%SELECTOR", selector);
            var response = getResponse(request);
            return response.Contains(@"""ok");
        }
    }

}
