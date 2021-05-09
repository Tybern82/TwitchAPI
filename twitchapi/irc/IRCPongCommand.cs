#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.irc {
    public class IRCPongCommand : IRCCommand {

        public IRCPongCommand() : base("PONG", "") { }

        public static void send(TwitchIRC irc) {
            new IRCPongCommand().sendCommand(irc);
        }
    }
}
