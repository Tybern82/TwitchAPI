#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.irc {
    public class IRCNickCommand : IRCCommand {

        public string Nickname { get; private set; }

        public IRCNickCommand(string name) : base ("NICK", name.ToLower()) {
            this.Nickname = name.ToLower();
        }

        public static void send(TwitchIRC irc, string name) {
            new IRCNickCommand(name).sendCommand(irc);
        }
    }
}
