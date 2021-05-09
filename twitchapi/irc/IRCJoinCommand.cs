#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.irc {
    public class IRCJoinCommand : IRCCommand {

        public string Channel { get; private set; }

        public IRCJoinCommand(string channel) : base("JOIN", "#" + channel.ToLower()) {
            this.Channel = channel;
        }

        public static void send(TwitchIRC irc, string channel) {
            new IRCJoinCommand(channel).sendCommand(irc);
        }
    }
}
