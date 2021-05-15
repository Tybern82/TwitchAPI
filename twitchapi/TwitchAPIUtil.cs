#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchAPI.twitchapi.auth;
using TwitchAPI.twitchapi.emotes;

namespace TwitchAPI.twitchapi {
    public class TwitchAPIUtil {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private OAuthBaseRequest oAuthToken;


        public TwitchAPIUtil(OAuthBaseRequest req) {
            this.oAuthToken = req;
        }

        public string GetEmoteURL(string setID, string emote) {
            string FORMAT = "https://static-cdn.jtvnw.net/emoticons/v2/{0}/default/dark/1.0";
            return string.Format(FORMAT, setID);
            /*
            GetEmotesetRequest req = new GetEmotesetRequest(setID);
            GetEmotesetResponse resp = req.doRequest(oAuthToken);
            if (!resp.Emotes.ContainsKey(emote)) return "";
            return resp.Emotes[emote];
            */
        }
    }
}
