#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.irc {
    public class IRCUserCommand : IRCCommand {

        public string Username { get; private set; }
        public string Hostname { get; private set; }
        public string ServerName { get; private set; }
        public string RealName { get; private set; }

        public IRCUserCommand(string uname, string host, string server, string real) : base(
            "USER", 
            uname + " " + host + " " + server + " :" + real
            ) {
            this.Username = uname;
            this.Hostname = host;
            this.ServerName = server;
            this.RealName = real;
        }

        public static void send(TwitchIRC irc, string uname, string real) {
            new IRCUserCommand(uname, "localhost", TwitchIRC.SERVER, real).sendCommand(irc);
        }
    }
}
