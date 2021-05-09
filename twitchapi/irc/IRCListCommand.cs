#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.irc {
    public class IRCListCommand : IRCCommand {

        public string[] Channels { get; private set; }

        public IRCListCommand(string[] ch) : base("LIST", string.Join(" ", ch)) {
            this.Channels = ch;
        }

        public static void send(TwitchIRC irc, string[] ch) {
            new IRCListCommand(ch).sendCommand(irc);
        }

        public static void send(TwitchIRC irc) {
            send(irc, new string[0]);
        }
    }
}
