#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi {
    public class TwitchAPIException : Exception { 
        public TwitchAPIException() : base() { }
        public TwitchAPIException(string message) : base(message) { }
    }
    public class TwitchAPIAuthenticationException: TwitchAPIException { 
        public TwitchAPIAuthenticationException() : base () { }
        public TwitchAPIAuthenticationException(string message) : base(message) { } 
    }
    public class TwitchAPIAccessDeniedException : TwitchAPIAuthenticationException { 
        public TwitchAPIAccessDeniedException() : base () { }
        public TwitchAPIAccessDeniedException(string message) : base(message) { } 
    }

    public class TwitchIRCException: Exception {
        public TwitchIRCException() : base () { }
        public TwitchIRCException(string message) : base (message) { }
    }
}
