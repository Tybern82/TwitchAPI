#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TwitchAPI.twitchapi.auth {

    public class OAuthAppAccessToken {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public TimeSpan ExpiresIn { get; set; }
        public TwitchScope Scope { get; set; }

        public OAuthAppAccessToken(string accessToken, string refreshToken, TimeSpan expiresIn, TwitchScope scope) {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
            Scope = scope;
        }

        public static OAuthAppAccessToken parseToken(string jsonData) {

            JObject token = JObject.Parse(jsonData);

            string AccessToken;
            string RefreshToken;
            TimeSpan ExpiresIn;
            TwitchScope Scope = TwitchScope.NONE;

            JToken? currToken = token["access_token"];
            if (currToken != null) {
                string? s = currToken.Value<string>();
                AccessToken = (s == null) ? "" : s;
            } else
                AccessToken = "";

            currToken = token["refresh_token"];
            if (currToken != null) {
                string? s = currToken.Value<string>();
                RefreshToken = (s == null) ? "" : s;
            } else
                RefreshToken = "";

            currToken = token["expires_in"];
            if (currToken != null) {
                int? secs = currToken.Value<int>();
                ExpiresIn = new TimeSpan(0, 0, (secs == null) ? 0 : (int)secs);
            } else
                ExpiresIn = new TimeSpan(0);

            currToken = token["scope"];
            if (currToken != null) {
                IEnumerable<string?> scopes = currToken.Values<string>();
                if (scopes != null) {
                    foreach (string? s in scopes) {
                        if ((s != null) && !string.IsNullOrWhiteSpace(s)) Scope |= TwitchScopeUtil.getScope(s);
                    }
                }
            }

            return new OAuthAppAccessToken(AccessToken, RefreshToken, ExpiresIn, Scope);
        }
    }
}
