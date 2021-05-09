#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.auth {
    public abstract class OAuthBaseRequest {
        public string ClientID { get { return TwitchAuthentication.ClientID; } }

        private TwitchScope _Scope = TwitchScope.NONE;
        public TwitchScope Scope { 
            get {
                return _Scope;
            }
            set {
                if (value != _Scope) {
                    _Scope = value;
                    clearToken();   // scope's changed, need to fully re-authenticate
                }
            }
        }

        private string? _accessCode;
        public string CommandAccessToken {
            get {
                if (_accessCode == null) {
                    if (this is OAuthClientCredentialsRequest) {
                        _accessCode = ((OAuthClientCredentialsRequest)this).RequestToken.AccessToken;
                    } else if (this is OAuthAuthorizationRequest) {
                        _accessCode = ((OAuthAuthorizationRequest)this).RequestToken.AccessToken;
                    } else if (this is OAuthImplicitRequest) {
                        _accessCode = ((OAuthImplicitRequest)this).AccessToken;
                    } else {
                        throw new TwitchAPIAuthenticationException("Unknown authentication type.");
                    }
                }
                return _accessCode;
            }
        }

        // TODO: public abstract void requestRefresh();
        public abstract void clearToken();
    }
}
