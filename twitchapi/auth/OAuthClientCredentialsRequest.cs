#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TwitchAPI.twitchapi.auth {
    public class OAuthClientCredentialsRequest : OAuthBaseRequest {

        private OAuthAppAccessToken? _RequestToken;
        public OAuthAppAccessToken RequestToken {
            get {
                if (_RequestToken == null) {
                    string requestURL = "https://id.twitch.tv/oauth2/token?client_id={0}&client_secret={1}&grant_type=client_credentials&scope={2}";
                    requestURL = string.Format(requestURL, new string[] {
                        Uri.EscapeDataString(ClientID),
                        Uri.EscapeDataString(TwitchAuthentication.ClientSecret),
                        Uri.EscapeDataString(TwitchScopeUtil.getScopeString(Scope))
                    });
                    WebRequest request = WebRequest.Create(requestURL);
                    request.Method = "POST";
                    request.ContentType = "text/plain";
                    request.ContentLength = 0;
                    request.GetRequestStream().Close();
                    WebResponse response = request.GetResponse();
                    string serverResponse = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                    response.Close();

                    _RequestToken = OAuthAppAccessToken.parseToken(serverResponse);
                }
                return _RequestToken;
            }
        }

        public override void clearToken() {
            _RequestToken = null;
        }
    }
}
