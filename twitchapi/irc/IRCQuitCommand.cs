#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.irc {
    public class IRCQuitCommand : IRCCommand {

        public string? QuitMessage { get; private set; }

        public IRCQuitCommand() : base("QUIT", "") { }

        public IRCQuitCommand(string msg) : base("QUIT", msg) {
            this.QuitMessage = msg;
        }

        public static void send(TwitchIRC irc) {
            new IRCQuitCommand().sendCommand(irc);
        }

        public static void send(TwitchIRC irc, string msg) {
            new IRCQuitCommand(msg).sendCommand(irc);
        }
    }
}
