#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.irc {
    public class IRCCommand {

        public string Command { get; private set; }
        public string Body { get; private set; }

        public IRCCommand(string cmd, string body) {
            this.Command = cmd;
            this.Body = body;
        }

        public void sendCommand(TwitchIRC irc) {
            irc.SendRawCommand(Command + (string.IsNullOrWhiteSpace(Body) ? "" : " " + Body));
        }
    }
}
