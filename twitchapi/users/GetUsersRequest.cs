#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using TwitchAPI.twitchapi.auth;

namespace TwitchAPI.twitchapi.users {
    public class GetUsersRequest : BaseRequest<GetUsersResponse> {

        public string[] UserIDs { get; set; }
        public string[] LoginNames { get; set; }

        public GetUsersRequest(string[] ids, string[] names, TwitchScope extraScope) : base("https://api.twitch.tv/helix/users", extraScope) {
            this.UserIDs = ids;
            this.LoginNames = names;
            this.Mode = RequestMode.GET;
        }

        public GetUsersRequest(string[] ids, string[] names) : this(ids, names, auth.TwitchScope.NONE) { }

        public GetUsersRequest(string uname) : this(new string[0], new string[] { uname }) { }

        protected override void loadRequest() {
            QueryParameters.Clear();
            BodyParameters.Clear();
            if (UserIDs.Length != 0) {
                List<string> ids = new List<string>(UserIDs);
                QueryParameters.Add("id", ids);
            }
            if (LoginNames.Length != 0) {
                List<string> names = new List<string>(LoginNames);
                QueryParameters.Add("login", names);
            }
        }

        protected override GetUsersResponse parseResponse(JObject data) {
            return new GetUsersResponse(data, this.RequiredScopes);
        }
    }

    public class GetUsersResponse {

        public List<UserDetails> Users { get; private set; }

        public GetUsersResponse(JToken jsonData, TwitchScope scope) {
            Users = new List<UserDetails>();
            JToken? currToken = jsonData["data"];
            if (currToken == null) throw new TwitchAPIException("Missing data in response.");
            JArray dataElement = (JArray)currToken;
            if (dataElement.Count < 1) throw new TwitchAPIException("No body to data in response.");
            foreach (JToken token in dataElement) {
                Users.Add(new UserDetails(token, scope));
            }
        }
    }

    public enum UserType { STAFF, ADMIN, GLOBAL_MOD, USER };
    public enum BroadcasterType { PARTNER, AFFILIATE, NORMAL };

    public class UserDetails {

        public string ID { get; private set; }
        public string Login { get; private set; }
        public string DisplayName { get; private set; }
        public BroadcasterType Broadcaster { get; private set; }
        public string Description { get; private set; }
        public string OfflineImageURL { get; private set; }
        public string ProfileImageURL { get; private set; }
        public UserType Type { get; private set; }
        public int ViewCount { get; private set; }
        public string? EMail { get; private set; }
        public string CreatedAt { get; private set; }

        public UserDetails(JToken jsonData, TwitchScope scope) {
            string? curr = jsonData.Value<string>("id");
            ID = (curr == null) ? "" : curr;

            curr = jsonData.Value<string>("login");
            Login = (curr == null) ? "" : curr;

            curr = jsonData.Value<string>("display_name");
            DisplayName = (curr == null) ? "" : curr;

            curr = jsonData.Value<string>("broadcaster_type");
            if (curr == null) curr = "";
            if (curr.Equals("partner")) {
                Broadcaster = BroadcasterType.PARTNER;
            } else if (curr.Equals("affiliate")) {
                Broadcaster = BroadcasterType.AFFILIATE;
            } else {
                Broadcaster = BroadcasterType.NORMAL;
            }

            curr = jsonData.Value<string>("description");
            Description = (curr == null) ? "" : curr;

            curr = jsonData.Value<string>("offline_image_url");
            OfflineImageURL = (curr == null) ? "" : curr;

            curr = jsonData.Value<string>("profile_image_url");
            ProfileImageURL = (curr == null) ? "" : curr;

            curr = jsonData.Value<string>("type");
            if (curr == null) curr = "";
            if (curr.Equals("staff")) {
                Type = UserType.STAFF;
            } else if (curr.Equals("admin")) {
                Type = UserType.ADMIN;
            } else if (curr.Equals("global_mod")) {
                Type = UserType.GLOBAL_MOD;
            } else {
                Type = UserType.USER;
            }

            ViewCount = jsonData.Value<int>("view_count");

            curr = jsonData.Value<string>("created_at");
            CreatedAt = (curr == null) ? "" : curr;

            if (scope.HasFlag(TwitchScope.USER_READ_EMAIL)) {
                curr = jsonData.Value<string>("email");
                EMail = (curr == null) ? "" : curr;
            }
        }
    }
}
