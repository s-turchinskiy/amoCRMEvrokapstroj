using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AmoDownloaderCLR_TestApp
{

    [XmlRoot(elementName: "Settings")]
    public class Settings
    {
        private static Settings _instance;
        private Settings()
        {
        }
        public void SetSettings(Settings settings)
        {
            _instance = settings;
        }
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }
        public string HostAmoCRM;
        public string ClientId;
        public string ClientSecret;
        public string Host1cDebugGet;
        public string Host1cDebugPost;
        public string Host1cReleaseGet;
        public string Host1cReleasePost;
    }
}
