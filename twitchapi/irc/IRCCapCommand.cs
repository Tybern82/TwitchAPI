using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchAPI.twitchapi.irc {
    public class IRCCapCommand : IRCCommand {

        public IRCCapCommand(string request) : base("CAP REQ", request) { }

        public static void send(TwitchIRC irc, string request) {
            new IRCCapCommand(request).sendCommand(irc);
        }
    }
}
