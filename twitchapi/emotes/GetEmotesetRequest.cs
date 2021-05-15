using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchAPI.twitchapi.emotes {
    public class GetEmotesetRequest : BaseRequest<GetEmotesetResponse> {

        public string SetID { get; private set; }

        public GetEmotesetRequest(string setID) : base("https://api.twitch.tv/kraken/chat/emoticon_images", auth.TwitchScope.NONE) {
            this.SetID = setID;
        }

        protected override void loadRequest() {
            QueryParameters.Clear();
            BodyParameters.Clear();
            QueryParameters.Add("emotesets", new List<string>(new string[] { SetID }));
        }

        protected override GetEmotesetResponse parseResponse(JObject data) {
            JToken? jsonData = data.Value<JToken>("emoticon_sets");
            if (jsonData == null) throw new ArgumentNullException();
            return new GetEmotesetResponse(data, SetID);
        }
    }

    public class GetEmotesetResponse {

        public Dictionary<string,string> Emotes { get; private set; }

        // http://static-cdn.jtvnw.net/emoticons/v1/EMOTE_ID/SIZE
        private static readonly string EMOTE_URL = "http://static-cdn.jtvnw.net/emoticons/v1/{0}/{1}";

        public GetEmotesetResponse(JToken jsonData, string setID) {
            JArray? emotes = jsonData.Value<JArray>(setID);
            if (emotes != null) {
                Emotes = new Dictionary<string, string>(emotes.Count);
                foreach (JToken? item in emotes) {
                    if (item != null) {
                        string? code = jsonData.Value<string>("code");
                        int ID = jsonData.Value<int>("id");
                        if (code != null) {
                            Emotes.Add(code, string.Format(EMOTE_URL, ID, "1.0"));
                        }
                    }
                }
            } else {
                Emotes = new Dictionary<string, string>();
            }
        }
    }
}