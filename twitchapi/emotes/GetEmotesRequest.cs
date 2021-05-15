#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchAPI.twitchapi.emotes {
    public class GetEmotesRequest : BaseRequest<GetEmotesResponse> {

        public GetEmotesRequest() : base("https://api.twitch.tv/kraken/chat/emoticons", auth.TwitchScope.NONE) { }

        protected override void loadRequest() {
            return; // nothing to load
        }

        protected override GetEmotesResponse parseResponse(JObject data) {
            return new GetEmotesResponse(data);
        }
    }

    public class GetEmotesResponse {

        public Dictionary<string, TwitchEmote> Emotes { get; private set; }

        public GetEmotesResponse(JToken jsonData) {
            JArray? emotes = jsonData.Value<JArray>("emoticons");
            if (emotes != null) {
                Emotes = new Dictionary<string, TwitchEmote>(emotes.Count);
                foreach (JToken? item in emotes) {
                    if (item != null) {
                        TwitchEmote e = new TwitchEmote(item);
                        Emotes.Add(e.Regex, e);
                    }
                }
            } else {
                Emotes = new Dictionary<string, TwitchEmote>();
            }
        }
    }
}
