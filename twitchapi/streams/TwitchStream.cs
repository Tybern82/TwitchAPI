#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchAPI.twitchapi.streams {
    public class TwitchStream {

        public string ID { get; private set; }          // id: Stream ID
        public string UserID { get; private set; }      // user_id: ID of the user who is streaming
        public string UserLogin { get; private set; }   // user_login: Login of the user who is streaming.
        public string UserName { get; private set; }    // user_name: Display name of the corresponding user_id
        public string GameID { get; private set; }      // game_id: ID of the game being played on the stream.
        public string GameName { get; private set; }    // game_name: Name of the game being played.
        public bool isLive { get; private set; }        // type: Stream type: "live" or "" (in case of error)
        public string Title { get; private set; }       // title: Stream title.
        public int ViewerCount { get; private set; }    // viewer_count: Number of viewers watching the stream at the time of the query.
        public DateTime StartedAt { get; private set; } // started_at: UTC timestamp
        public string Language { get; private set; }    // language: Stream language. A language value is either the ISO 639-1 two-letter code for a supported stream language or "other"
        public string ThumbnailURL { get; private set; }// thumbnail_url: Thumbnail URL of the stream. All image URLs have variable width and height. You can replace {width} and {height} with any values to get that sized image.
        public List<string> TagIDs { get; private set; }// tag_ids: Shows tag IDs that apply to the stream.
        public bool isMature { get; private set; }      // is_mature: Indicates if the broadcaster has specified their channel contains mature content that may be inappropriate for younger audiences.
        public string Cursor { get; private set; }      // pagination->cursor: object containing a string, A cursor value, to be used in a subsequent request to specify the starting point of the next set of results.

        public TwitchStream(JToken jsonData) {
            ID = jsonData.Value<string>("id") ?? "";
            UserID = jsonData.Value<string>("user_id") ?? "";
            UserLogin = jsonData.Value<string>("user_login") ?? "";
            UserName = jsonData.Value<string>("user_name") ?? "";
            GameID = jsonData.Value<string>("game_id") ?? "";
            GameName = jsonData.Value<string>("game_name") ?? "";
            string liveStr = jsonData.Value<string>("type") ?? "";
            isLive = (liveStr.Equals("live", StringComparison.OrdinalIgnoreCase));
            Title = jsonData.Value<string>("title") ?? "";
            ViewerCount = jsonData.Value<int>("viewer_count");
            DateTime? startTime = jsonData.Value<DateTime>("started_at");
            StartedAt = (startTime == null) ? DateTime.Now : (DateTime)startTime;
            Language = jsonData.Value<string>("language") ?? "";
            ThumbnailURL = jsonData.Value<string>("thumbnail_url") ?? "";
            JArray? tags = jsonData.Value<JArray>("tag_ids");
            if (tags != null) {
                TagIDs = new List<string>(tags.Count);
                foreach (JToken curr in tags) {
                    string? item = curr.Value<string>();
                    if (item != null) TagIDs.Add(item);
                }
            } else {
                TagIDs = new List<string>();
            }
            isMature = jsonData.Value<bool>("is_mature");
            Cursor = "";
            JToken? page = jsonData.Value<string>("pagination");
            if (page != null) {
                if (page.HasValues) Cursor = page.Value<string>("cursor") ?? "";
            }
        }
    }
}
