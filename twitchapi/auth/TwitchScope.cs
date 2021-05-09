#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchAPI.twitchapi.auth {
    [Flags] 
    public enum TwitchScope {
        NONE                        = 0,        // 

        ANALYTICS_READ_EXTENSIONS   = 1,        // View analytics data for the Twitch Extensions owned by the authenticated account.
        ANALYTICS_READ_GAMES        = 2,        // View analytics data for the games owned by the authenticated account.
        BITS_READ                   = 4,        // View Bits information for a channel.
        CHANNEL_EDIT_COMMERCIAL     = 8,        // Run commercials on a channel.
        CHANNEL_MANAGE_BROADCAST    = 16,       // Manage a channel’s broadcast configuration, including updating channel configuration and managing stream markers and stream tags.
        CHANNEL_MANAGE_EXTENSIONS   = 32,       // Manage a channel’s Extension configuration, including activating Extensions.
        CHANNEL_MANAGE_REDEMPTIONS  = 64,       // Manage Channel Points custom rewards and their redemptions on a channel.
        CHANNEL_MANAGE_VIDEOS       = 128,      // Manage a channel’s videos, including deleting videos.
        CHANNEL_READ_EDITORS        = 256,      // View a list of users with the editor role for a channel.
        CHANNEL_READ_HYPETRAIN      = 512,      // View Hype Train information for a channel.
        CHANNEL_READ_REDEMPTIONS    = 1024,     // View Channel Points custom rewards and their redemptions on a channel.
        CHANNEL_READ_STREAMKEY      = 2048,     // View an authorized user’s stream key.
        CHANNEL_READ_SUBSCRIPTIONS  = 4096,     // View a list of all subscribers to a channel and check if a user is subscribed to a channel.
        CLIPS_EDIT                  = 8192,     // Manage Clips for a channel.
        MODERATION_READ             = 16384,    // View a channel’s moderation data including Moderators, Bans, Timeouts, and Automod settings.
        USER_EDIT                   = 32768,    // Manage a user object.
        USER_EDIT_FOLLOWS           = 65536,    // Edit a user’s follows.
        USER_MANAGE_BLOCKEDUSERS    = 131072,   // Manage the block list of a user.
        USER_READ_BLOCKEDUSERS      = 262144,   // View the block list of a user.
        USER_READ_BROADCAST         = 524288,   // View a user’s broadcasting configuration, including Extension configurations.
        USER_READ_FOLLOWS           = 1048576,  // View the list of channels a user follows.
        USER_READ_SUBSCRIPTIONS     = 2097152,  // View if an authorized user is subscribed to specific channels.

        // Chat / PubSub Scopes
        CHANNEL_MODERATE            = 4194304,  // Perform moderation actions in a channel.The user requesting the scope must be a moderator in the channel.
        CHAT_EDIT                   = 8388608,  // Send live stream chat and rooms messages.
        CHAT_READ                   = 16777216, // View live stream chat and rooms messages.
        WHISPERS_READ               = 33554432, // View your whisper messages.
        WHISPERS_EDIT               = 67108864, // Send whisper messages.

        USER_READ_EMAIL             = 134217728 // Read user's email in response.
    }

    public class TwitchScopeUtil {
        public static string getScopeString(TwitchScope scopes) {
            string _result = ""; bool isFirst = true;
            if (scopes.HasFlag(TwitchScope.ANALYTICS_READ_EXTENSIONS)) { _result += (isFirst ? "" : " ") + "analytics:read:extensions"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.ANALYTICS_READ_GAMES)) { _result += (isFirst ? "" : " ") + "analytics:read:games"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.BITS_READ)) { _result += (isFirst ? "" : " ") + "bits:read"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_EDIT_COMMERCIAL)) { _result += (isFirst ? "" : " ") + "channel:edit:commercial"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_MANAGE_BROADCAST)) { _result += (isFirst ? "" : " ") + "channel:manage:broadcast"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_MANAGE_EXTENSIONS)) { _result += (isFirst ? "" : " ") + "channel:manage:extensions"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_MANAGE_REDEMPTIONS)) { _result += (isFirst ? "" : " ") + "channel:manage:redemptions"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_MANAGE_VIDEOS)) { _result += (isFirst ? "" : " ") + "channel:manage:videos"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_EDITORS)) { _result += (isFirst ? "" : " ") + "channel:read:editors"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_HYPETRAIN)) { _result += (isFirst ? "" : " ") + "channel:read:hype_train"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_REDEMPTIONS)) { _result += (isFirst ? "" : " ") + "channel:read:redemptions"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_STREAMKEY)) { _result += (isFirst ? "" : " ") + "channel:read:stream_key"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_SUBSCRIPTIONS)) { _result += (isFirst ? "" : " ") + "channel:read:subscriptions"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CLIPS_EDIT)) { _result += (isFirst ? "" : " ") + "clips:edit"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.MODERATION_READ)) { _result += (isFirst ? "" : " ") + "moderation:read"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.USER_EDIT)) { _result += (isFirst ? "" : " ") + "user:edit"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.USER_EDIT_FOLLOWS)) { _result += (isFirst ? "" : " ") + "user:edit:follows"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.USER_MANAGE_BLOCKEDUSERS)) { _result += (isFirst ? "" : " ") + "user:manage:blocked_users"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.USER_READ_BLOCKEDUSERS)) { _result += (isFirst ? "" : " ") + "user:read:blocked_users"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.USER_READ_BROADCAST)) { _result += (isFirst ? "" : " ") + "user:read:broadcast"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.USER_READ_FOLLOWS)) { _result += (isFirst ? "" : " ") + "user:read:follows"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.USER_READ_SUBSCRIPTIONS)) { _result += (isFirst ? "" : " ") + "user:read:subscriptions"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHANNEL_MODERATE)) { _result += (isFirst ? "" : " ") + "channel:moderate"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHAT_EDIT)) { _result += (isFirst ? "" : " ") + "chat:edit"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.CHAT_READ)) { _result += (isFirst ? "" : " ") + "chat:read"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.WHISPERS_READ)) { _result += (isFirst ? "" : " ") + "whispers:read"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.WHISPERS_EDIT)) { _result += (isFirst ? "" : " ") + "whispers:edit"; isFirst = false; }
            if (scopes.HasFlag(TwitchScope.USER_READ_EMAIL)) { _result += (isFirst ? "" : " ") + "user:read:email"; isFirst = false; }
            return _result;
        }

        public static List<TwitchScope> getIndividualScopes(TwitchScope scopes) {
            List<TwitchScope> _result = new List<TwitchScope>();
            if (scopes.HasFlag(TwitchScope.ANALYTICS_READ_EXTENSIONS)) _result.Add(TwitchScope.ANALYTICS_READ_EXTENSIONS);
            if (scopes.HasFlag(TwitchScope.ANALYTICS_READ_GAMES)) _result.Add(TwitchScope.ANALYTICS_READ_GAMES);
            if (scopes.HasFlag(TwitchScope.BITS_READ)) _result.Add(TwitchScope.BITS_READ);
            if (scopes.HasFlag(TwitchScope.CHANNEL_EDIT_COMMERCIAL)) _result.Add(TwitchScope.CHANNEL_EDIT_COMMERCIAL);
            if (scopes.HasFlag(TwitchScope.CHANNEL_MANAGE_BROADCAST)) _result.Add(TwitchScope.CHANNEL_MANAGE_BROADCAST);
            if (scopes.HasFlag(TwitchScope.CHANNEL_MANAGE_EXTENSIONS)) _result.Add(TwitchScope.CHANNEL_MANAGE_EXTENSIONS);
            if (scopes.HasFlag(TwitchScope.CHANNEL_MANAGE_REDEMPTIONS)) _result.Add(TwitchScope.CHANNEL_MANAGE_REDEMPTIONS);
            if (scopes.HasFlag(TwitchScope.CHANNEL_MANAGE_VIDEOS)) _result.Add(TwitchScope.CHANNEL_MANAGE_VIDEOS);
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_EDITORS)) _result.Add(TwitchScope.CHANNEL_READ_EDITORS);
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_HYPETRAIN)) _result.Add(TwitchScope.CHANNEL_READ_HYPETRAIN);
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_REDEMPTIONS)) _result.Add(TwitchScope.CHANNEL_READ_REDEMPTIONS);
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_STREAMKEY)) _result.Add(TwitchScope.CHANNEL_READ_STREAMKEY);
            if (scopes.HasFlag(TwitchScope.CHANNEL_READ_SUBSCRIPTIONS)) _result.Add(TwitchScope.CHANNEL_READ_SUBSCRIPTIONS);
            if (scopes.HasFlag(TwitchScope.CLIPS_EDIT)) _result.Add(TwitchScope.CLIPS_EDIT);
            if (scopes.HasFlag(TwitchScope.MODERATION_READ)) _result.Add(TwitchScope.MODERATION_READ);
            if (scopes.HasFlag(TwitchScope.USER_EDIT)) _result.Add(TwitchScope.USER_EDIT);
            if (scopes.HasFlag(TwitchScope.USER_EDIT_FOLLOWS)) _result.Add(TwitchScope.USER_EDIT_FOLLOWS);
            if (scopes.HasFlag(TwitchScope.USER_MANAGE_BLOCKEDUSERS)) _result.Add(TwitchScope.USER_MANAGE_BLOCKEDUSERS);
            if (scopes.HasFlag(TwitchScope.USER_READ_BLOCKEDUSERS)) _result.Add(TwitchScope.USER_READ_BLOCKEDUSERS);
            if (scopes.HasFlag(TwitchScope.USER_READ_BROADCAST)) _result.Add(TwitchScope.USER_READ_BROADCAST);
            if (scopes.HasFlag(TwitchScope.USER_READ_FOLLOWS)) _result.Add(TwitchScope.USER_READ_FOLLOWS);
            if (scopes.HasFlag(TwitchScope.USER_READ_SUBSCRIPTIONS)) _result.Add(TwitchScope.USER_READ_SUBSCRIPTIONS);
            if (scopes.HasFlag(TwitchScope.CHANNEL_MODERATE)) _result.Add(TwitchScope.CHANNEL_MODERATE);
            if (scopes.HasFlag(TwitchScope.CHAT_EDIT)) _result.Add(TwitchScope.CHAT_EDIT);
            if (scopes.HasFlag(TwitchScope.CHAT_READ)) _result.Add(TwitchScope.CHAT_READ);
            if (scopes.HasFlag(TwitchScope.WHISPERS_READ)) _result.Add(TwitchScope.WHISPERS_READ);
            if (scopes.HasFlag(TwitchScope.WHISPERS_EDIT)) _result.Add(TwitchScope.WHISPERS_EDIT);
            if (scopes.HasFlag(TwitchScope.USER_READ_EMAIL)) _result.Add(TwitchScope.USER_READ_EMAIL);
            return _result;
        }

        public static TwitchScope getScope(string scopes) {
            return getScope(scopes.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            // return getScope(scopes.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        public static TwitchScope getScope(string[] scopes) {
            TwitchScope _result = 0;
            foreach (string s in scopes) _result |= decodeScope(s);
            return _result;
        }

        public static TwitchScope getScope(IEnumerable<TwitchScope> scopes) {  // used to get
            TwitchScope _result = 0;
            foreach (TwitchScope s in scopes) _result |= s;
            return _result;
        }

        private static TwitchScope decodeScope(string scope) {
            if (string.IsNullOrWhiteSpace(scope)) return 0;
            scope = scope.Trim();
            if (scope.Equals("analytics:read:extensions")) return TwitchScope.ANALYTICS_READ_EXTENSIONS;
            if (scope.Equals("analytics:read:games")) return TwitchScope.ANALYTICS_READ_GAMES;
            if (scope.Equals("bits:read")) return TwitchScope.BITS_READ;
            if (scope.Equals("channel:edit:commercial")) return TwitchScope.CHANNEL_EDIT_COMMERCIAL;
            if (scope.Equals("channel:manage:broadcast")) return TwitchScope.CHANNEL_MANAGE_BROADCAST;
            if (scope.Equals("channel:manage:extensions")) return TwitchScope.CHANNEL_MANAGE_EXTENSIONS;
            if (scope.Equals("channel:manage:redemptions")) return TwitchScope.CHANNEL_MANAGE_REDEMPTIONS;
            if (scope.Equals("channel:manage:videos")) return TwitchScope.CHANNEL_MANAGE_VIDEOS;
            if (scope.Equals("channel:read:editors")) return TwitchScope.CHANNEL_READ_EDITORS;
            if (scope.Equals("channel:read:hype_train")) return TwitchScope.CHANNEL_READ_HYPETRAIN;
            if (scope.Equals("channel:read:redemptions")) return TwitchScope.CHANNEL_READ_REDEMPTIONS;
            if (scope.Equals("channel:read:stream_key")) return TwitchScope.CHANNEL_READ_STREAMKEY;
            if (scope.Equals("channel:read:subscriptions")) return TwitchScope.CHANNEL_READ_SUBSCRIPTIONS;
            if (scope.Equals("clips:edit")) return TwitchScope.CLIPS_EDIT;
            if (scope.Equals("moderation:read")) return TwitchScope.MODERATION_READ;
            if (scope.Equals("user:edit")) return TwitchScope.USER_EDIT;
            if (scope.Equals("user:edit:follows")) return TwitchScope.USER_EDIT_FOLLOWS;
            if (scope.Equals("user:manage:blocked_users")) return TwitchScope.USER_MANAGE_BLOCKEDUSERS;
            if (scope.Equals("user:read:blocked_users")) return TwitchScope.USER_READ_BLOCKEDUSERS;
            if (scope.Equals("user:read:broadcast")) return TwitchScope.USER_READ_BROADCAST;
            if (scope.Equals("user:read:follows")) return TwitchScope.USER_READ_FOLLOWS;
            if (scope.Equals("user:read:subscriptions")) return TwitchScope.USER_READ_SUBSCRIPTIONS;
            if (scope.Equals("channel:moderate")) return TwitchScope.CHANNEL_MODERATE;
            if (scope.Equals("chat:edit")) return TwitchScope.CHAT_EDIT;
            if (scope.Equals("chat:read")) return TwitchScope.CHAT_READ;
            if (scope.Equals("whispers:read")) return TwitchScope.WHISPERS_READ;
            if (scope.Equals("whispers:edit")) return TwitchScope.WHISPERS_EDIT;
            if (scope.Equals("user:read:email")) return TwitchScope.USER_READ_EMAIL;
            return 0;
        }
    }
}