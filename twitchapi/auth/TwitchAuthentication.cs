#nullable enable

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TwitchAPI.twitchapi.auth {
    public class TwitchAuthentication {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static readonly string ClientID = "yodb4mx7oif8l4un9jyd7uw6qhz8gn";
        public static readonly string ClientSecret = "o9zrfl15h4gwa2hm4igvm2ai5mhci2";
        public static readonly string RedirectURI = "http://localhost:8000/twitchAccessToken";

        // Opens url in browser and starts local server to receive the access token
        public static async Task<string> Authenticate(string url) {
            try {
                Uri startUri = new HttpRequestMessage(HttpMethod.Get, url).RequestUri;
                Uri endUri = new Uri(RedirectURI);
                Task<string> _token = TwitchAuthenticationServer.Start(new string[0]);
                Logger.Info("Opening: " + startUri.AbsoluteUri); 
                try {
                    OpenUrl(startUri.AbsoluteUri);
                } catch (System.Exception other) {
                    Logger.Warn(other.Message);
                }
                string token = await _token;
                if ((TwitchAuthenticationServer.errString == null) || string.IsNullOrWhiteSpace(TwitchAuthenticationServer.errString))
                    return token;
                else
                    throw new TwitchAPIAuthenticationException(TwitchAuthenticationServer.errString);
            } catch (Exception e) {
                Logger.Warn(e.ToString());
            }
            return string.Empty;
        }

        private static void OpenUrl(string url) {
            try {
                Process.Start(url);
            } catch {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                    Process.Start("xdg-open", url);
                } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                    Process.Start("open", url);
                } else {
                    throw;
                }
            }
        }
    }
}
