#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TwitchAPI.twitchapi.auth {
    public class OAuthAuthorizationRequest : OAuthClientRequest {

        public override string ResponseType { get { return "code"; } }

        private OAuthAppAccessToken? _RequestToken;
        public OAuthAppAccessToken RequestToken {
            get {
                if (_RequestToken == null) {
                    string requestURL = "https://id.twitch.tv/oauth2/token?client_id={0}&client_secret={1}&code={2}&grant_type=authorization_code&redirect_uri={3}";
                    requestURL = string.Format(requestURL, new string[] {
                        Uri.EscapeDataString(ClientID),
                        Uri.EscapeDataString(TwitchAuthentication.ClientSecret),
                        Uri.EscapeDataString(AccessToken),
                        Uri.EscapeDataString(RedirectURI)
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
            _AccessToken = null;
        }
    }
}