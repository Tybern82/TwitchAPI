#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.auth {
    public class OAuthImplicitRequest : OAuthClientRequest {
        public override string ResponseType { get { return "token"; } }

        public override void clearToken() {
            _AccessToken = null;
        }
    }
}
