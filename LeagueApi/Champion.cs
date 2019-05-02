using System;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace LeagueApi
{
    public class Champion
    {
        private WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
        public ChampionList champions = new ChampionList();
        public ChampionData getChampionById(int championId)
        {
            foreach(ChampionData data in champions.data.Values) if (championId == data.key) return data;
            return null;
        }
        public ChampionData getChampionByName(string championName)
        {
            foreach (ChampionData data in champions.data.Values) if (championName == data.name) return data;
            return null;
        }
        public Champion(string language)
        {
            string ChampJSON;
        GetChamp:
            try
            {
                ChampJSON = wc.DownloadString("http://ddragon.leagueoflegends.com/cdn/9.9.1/data/" + language + "/champion.json");
            }
            catch (WebException e)
            {
                var resp = (HttpWebResponse)e.Response;
                if (resp.StatusCode == HttpStatusCode.NotFound || resp.StatusCode == HttpStatusCode.Forbidden) throw;
                System.Threading.Thread.Sleep(1000 / 60);
                goto GetChamp;
            }
            champions = JsonConvert.DeserializeObject<ChampionList>(ChampJSON);
        }
        [Serializable]
        public class ChampionList
        {
            public string type;
            public string format;
            public string version;
            public Dictionary<string, ChampionData> data;
        }
        [Serializable]
        public class ChampionData
        {
            public string version;
            public string id;
            public int key;
            public string name;
            public string title;
            public string blurb;
            public ChampionInfo info;
            public ChampionImage image;
            public string[] tags;
            public string partype;
            public ChampionStats stats;
        }
        [Serializable]
        public class ChampionInfo
        {
            public int attack;
            public int defense;
            public int magic;
            public int difficulty;
        }
        [Serializable]
        public class ChampionImage
        {
            public string full;
            public string sprite;
            public string group;
            public int x;
            public int y;
            public int w;
            public int h;
        }
        [Serializable]
        public class ChampionStats
        {
            public float hp;
            public float hpperLevel;
            public float mp;
            public float mpperLevel;
            public float movespeed;
            public float armor;
            public float armorperLevel;
            public float spellblock;
            public float spellblockperLevel;
            public float attackrange;
            public float hpregen;
            public float hpregenperLevel;
            public float crit;
            public float critperLevel;
            public float attackdamage;
            public float attackdamageperLevel;
            public float attackspeedoffset;
            public float attackspeedperLevel;
        }
    }
}
