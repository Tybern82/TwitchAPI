#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using TwitchAPI.twitchapi.auth;

namespace TwitchAPI.twitchapi.irc {
    public class IRCPassCommand : IRCCommand {

        public string Password { get; private set; }

        public IRCPassCommand(string oAuth) : base("PASS", "oauth:"+oAuth) { this.Password = oAuth; }
        public IRCPassCommand(OAuthBaseRequest req) : this(req.CommandAccessToken) { }

        public static void send(TwitchIRC irc, string oAuth) {
            new IRCPassCommand(oAuth).sendCommand(irc);
        }

        public static void send(TwitchIRC irc, OAuthBaseRequest req) {
            new IRCPassCommand(req).sendCommand(irc);
        }
    }
}
