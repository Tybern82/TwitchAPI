#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.irc {
    public class IRCPartCommand : IRCCommand {

        public string Channel { get; private set; }

        public IRCPartCommand(string channel) : base("PART", "#" + channel.ToLower()) {
            this.Channel = channel;
        }

        public static void send(TwitchIRC irc, string channel) {
            new IRCPartCommand(channel).sendCommand(irc);
        }
    }
}
