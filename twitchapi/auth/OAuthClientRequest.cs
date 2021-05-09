#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.auth {
    public abstract class OAuthClientRequest : OAuthBaseRequest {
        public string RedirectURI { get { return TwitchAuthentication.RedirectURI; } }
        public abstract string ResponseType { get; }

        public bool? ForceVerify { get; set; }
        public string? State { get; set; }

        protected string? _AccessToken = null;
        public string AccessToken {
            get {
                if (_AccessToken == null) _AccessToken = TwitchAuthentication.Authenticate(getRequestString()).Result;
                return _AccessToken;
            }
        }

        public string getRequestString() {
            string _result = "https://id.twitch.tv/oauth2/authorize";
            _result += "?client_id=" + Uri.EscapeDataString(ClientID);
            _result += "&redirect_uri=" + Uri.EscapeDataString(RedirectURI);
            _result += "&response_type=" + Uri.EscapeDataString(ResponseType);
            _result += "&scope=" + Uri.EscapeDataString(TwitchScopeUtil.getScopeString(Scope));
            if (ForceVerify != null) _result += "&force_verify=" + Uri.EscapeDataString("" + ForceVerify);
            if (State != null) _result += "&state=" + Uri.EscapeDataString(State);
            return _result;
        }
    }
}
