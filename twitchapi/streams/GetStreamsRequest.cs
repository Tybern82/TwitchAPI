#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchAPI.twitchapi.streams {
    public class GetStreamsRequest : BaseRequest<List<TwitchStream>> {

        private static readonly string GET_STREAMS_REQUEST = "https://api.twitch.tv/helix/streams";

        public string? After { get; set; }  // Cursor for forward pagination: tells the server where to start fetching the next set of results,
                                            // in a multi-page response. The cursor value specified here is from the pagination response field of
                                            // a prior query.

        public string? Before { get; set; } // Cursor for backward pagination: tells the server where to start fetching the next set of results,
                                            // in a multi-page response. The cursor value specified here is from the pagination response field
                                            // of a prior query.

        private int _First = 20;
        public int First { 
            get { return _First; }
            set {
                _First = (value > 100) ? 100 : (value <= 0) ? 1 : value;
            } 
        }      // Maximum number of objects to return. Maximum: 100, Default: 20

        public List<string> GameID { get; private set; }    // Returns streams broadcasting a specified game ID. You can specify up to 100 IDs

        public List<string> Language { get; private set; }  // Stream language. You can specify up to 100 languages. A language value must be either the
                                                            // ISO 639-1 two-letter code for a supported stream language or "other".

        public List<string> UserID { get; private set; }    // Returns streams broadcast by one or more specified user IDs. You can specify up to 100 IDs.

        public List<string> UserLogin { get; private set; } // Returns streams broadcast by one or more specified user login names. You can specify up to 100 names.

        public GetStreamsRequest() : base(GET_STREAMS_REQUEST, auth.TwitchScope.NONE) {
            this.Mode = RequestMode.GET;
            this.GameID = new List<string>();
            this.Language = new List<string>();
            this.UserID = new List<string>();
            this.UserLogin = new List<string>();
        }

        protected override void loadRequest() {
            QueryParameters.Clear();
            BodyParameters.Clear();
            if (After != null) QueryParameters.Add("after", new List<string>(new string[] { After }));
            if (Before != null) QueryParameters.Add("before", new List<string>(new string[] { Before }));
            if (First != 20) QueryParameters.Add("first", new List<string>(new string[] { First+"" }));
            if (GameID.Count != 0) QueryParameters.Add("game_id", GameID);
            if (Language.Count != 0) QueryParameters.Add("language", Language);
            if (UserID.Count != 0) QueryParameters.Add("user_id", UserID);
            if (UserLogin.Count != 0) QueryParameters.Add("user_login", UserLogin);
        }

        protected override List<TwitchStream> parseResponse(JObject data) {
            JArray? streams = data.Value<JArray>("data");
            if (streams != null) {
                List<TwitchStream> _result = new List<TwitchStream>(streams.Count);
                foreach (JToken item in streams) {
                    _result.Add(new TwitchStream(item));
                }
                return _result;
            } else {
                return new List<TwitchStream>();
            }
        }
    }
}