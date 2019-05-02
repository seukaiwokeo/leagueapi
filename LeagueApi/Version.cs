using System;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace LeagueApi
{
    public class Version
    {
        private WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
        public VersionList versions = new VersionList();
        public string currentVersion = "null";
        public string previousVersion = "null";
        public Version()
        {
            string VersionJSON;
        GetVersions:
            try
            {
                VersionJSON = wc.DownloadString("https://ddragon.leagueoflegends.com/api/versions.json");
            }
            catch (WebException e)
            {
                var resp = (HttpWebResponse)e.Response;
                if (resp.StatusCode == HttpStatusCode.NotFound || resp.StatusCode == HttpStatusCode.Forbidden) throw;
                System.Threading.Thread.Sleep(1000 / 60);
                goto GetVersions;
            }
            versions = JsonConvert.DeserializeObject<VersionList>("{\"version\":" + VersionJSON + "}");
            currentVersion = versions.version[0];
            previousVersion = versions.version[1];
        }
        [Serializable]
        public class VersionList
        {
            public string[] version;
        }
    }
}
