using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
namespace LeagueApi
{
    public class Summoner
    {

        private WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
        private string[] ProfileIconBase = { "https://opgg-static.akamaized.net/images/profile_icons/profileIcon", ".jpg" };
        private string[] SummonerBase = { "https://", ".api.riotgames.com/lol/summoner/v4/summoners/by-name/", "?api_key=" };
        private string[] LeagueBase = { "https://", ".api.riotgames.com/lol/league/v4/positions/by-summoner/", "?api_key=" };
        private string[] MatchesBase = { "https://", ".api.riotgames.com/lol/match/v4/matchlists/by-account/", "?api_key=" };
        /* Public */
        public SummonerInfo info = new SummonerInfo();
        public SummonerLeagues leagues  = new SummonerLeagues();
        public SummonerMatches matches = new SummonerMatches();
        public Summoner(string summonerName, string API_KEY, string region) {
            string SummonerJSON = wc.DownloadString(SummonerBase[0] + region + SummonerBase[1] + summonerName + SummonerBase[2] + API_KEY);
            info = JsonConvert.DeserializeObject<SummonerInfo>(SummonerJSON);
            info.profileIconURL = "https://opgg-static.akamaized.net/images/profile_icons/profileIcon" + info.profileIconId.ToString() + ".jpg";
            string LeagueJSON;
            string MatchesJSON;
        GetLeague:
            try
            {
                LeagueJSON = wc.DownloadString(LeagueBase[0] + region + LeagueBase[1] + info.id + LeagueBase[2] + API_KEY);
            } catch(Exception e)
            {
                System.Threading.Thread.Sleep(1000 / 60);
                goto GetLeague;
            }
            leagues = JsonConvert.DeserializeObject<SummonerLeagues>("{\"league\":" + LeagueJSON + "}");
        GetMatches:
            try
            {
                MatchesJSON = wc.DownloadString(MatchesBase[0] + region + MatchesBase[1] + info.accountId + MatchesBase[2] + API_KEY);
            }
            catch (Exception e)
            {
                System.Threading.Thread.Sleep(1000 / 60);
                goto GetMatches;
            }
            matches = JsonConvert.DeserializeObject<SummonerMatches>(MatchesJSON.Replace("matches\":[{", "match\":[{"));
        }
        [Serializable]
        public class SummonerInfo
        {
            public string id;
            public string accountId;
            public string puuid;
            public string name;
            public int profileIconId;
            public long revisionDate;
            public long summonerLevel;
            public string profileIconURL;
        }
        [Serializable]
        public class SummonerLeagues
        {
            public SummonerLeague[] league;
        }
        [Serializable]
        public class MiniSeriesDTO
        {
            public int target;
            public int wins;
            public int losses;
            public string progress;
        }
        [Serializable]
        public class SummonerLeague
        {
            public string leagueId;
            public string leagueName;
            public string queueType;
            public string position;
            public string tier;
            public string rank;
            public string summonerId;
            public string summonerName;
            public int leaguePoints;
            public int wins;
            public int losses;
            public bool veteran;
            public bool inactive;
            public bool freshBlood;
            public bool hotStreak;
            public MiniSeriesDTO miniSeries;
        }
        [Serializable]
        public class SummonerMatches
        {
            public SummonerMatch[] match;
            public int totalGames;
            public int startIndex;
            public int endIndex;
        }
        [Serializable]
        public class SummonerMatch
        {
            public string lane;
            public long gameId;
            public int champion;
            public string platformId;
            public int season;
            public int queue;
            public string role;
            public long timestamp;
        }
    }
}