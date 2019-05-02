using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace LeagueApi
{
    public class ChampionRotation
    {
        private WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
        private string[] RotationBase = { "https://", ".api.riotgames.com/lol/platform/v3/champion-rotations?api_key=" };
        public ChampionInfo info = new ChampionInfo();
        public ChampionRotation(string API_KEY, string region) {
            string RotationJSON;
        GetRotation:
            try
            {
                RotationJSON = wc.DownloadString(RotationBase[0] + region + RotationBase[1] + API_KEY);
            }
            catch (WebException e)
            {
                var resp = (HttpWebResponse)e.Response;
                if (resp.StatusCode == HttpStatusCode.NotFound || resp.StatusCode == HttpStatusCode.Forbidden) throw;
                System.Threading.Thread.Sleep(1000 / 60);
                goto GetRotation;
            }
            info = JsonConvert.DeserializeObject<ChampionInfo>(RotationJSON);
        }
        [Serializable]
        public class ChampionInfo
        {
            public int[] freeChampionIdsForNewPlayers;
            public int[] freeChampionIds;
            public int maxNewPlayerLevel;
        }
    }
}
