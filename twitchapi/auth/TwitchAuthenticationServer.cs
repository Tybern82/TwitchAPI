#nullable enable

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwitchAPI.twitchapi.auth {
    class TwitchAuthenticationServer {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static HttpListener? listener;
        public static string url = "http://localhost:8000/";
        public static string twitchURL = "/twitchAccessToken";
        public static string? accessToken = null;
        public static string? errString = null;

        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string pageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>Server waiting for Twitch Authentication</title>" +
            "  </head>" +
            "  <body>" +
            "    <p>Page Views: {0}</p>" +
            "    <form method=\"post\" action=\"shutdown\">" +
            "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
            "    </form>" +
            "  </body>" +
            "</html>";

        public static string tokenRedirect =
            "<!DOCTYPE>" +
            "<html>" +
            "   <head>" +
            "      <title>Access Token Received</title>" +
            "      <script> " +
            "         function onLoad() {" +
            "            console.log(\"Running onLoad: \" + window.location.href); \n" +
            "            var url = new URLSearchParams(window.location.href); \n" +
            "            if (!url.has(\"uri\")) { \n" +
            "               var urlhash = window.location.hash,  //get the hash from url \n" +
            "               txthash = urlhash.replace(\"#\", \"\");  //remove the # \n" +
            "               console.log(\"Redirecting: \" + txthash); \n" +
            "               window.location.replace(\"http://localhost:8000/twitchAccessToken?uri=\" + txthash); \n" +
            "            } \n " +
            "         } \n" +
            "      </script> " +
            "   </head>" +
            "   <body onload=\"onLoad()\">" +
            "      <h1>Redirecting, please wait...</h1>" +
            "   </body>" +
            "</html>";

        public static string tokenSuccess =
            "<!DOCTYPE>" +
            "<html>" +
            "   <head>" +
            "      <title>Access Token Received</title>" +
            "   </head>" +
            "   <body>" +
            "      <h1>You may now close this window.</h1>" +
            "   </body>" +
            "</html>";


        public static async Task HandleIncomingConnections() {
            bool runServer = true;
            if (listener == null) return;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer) {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print out some info about the request
                Logger.Trace("Request #: {0}", ++requestCount);
                Logger.Trace(req.Url.ToString());
                Logger.Trace(req.HttpMethod);
                Logger.Trace(req.UserHostName);
                Logger.Trace(req.UserAgent);

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown")) {
                    Logger.Trace("Shutdown requested");
                    runServer = false;
                }
                byte[] data;
                if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath.StartsWith(twitchURL))) {
                    if (!string.IsNullOrWhiteSpace(req.Url.Query)) {
                        Logger.Trace("Requested Access Token: " + req.Url.Query);
                        Match m = Regex.Match(req.Url.Query, "access_token=(?<token>.*)&scope=");
                        if (m.Success) accessToken = m.Groups["token"].Value;
                        // if (m.Success && m.Groups.ContainsKey("token")) accessToken = m.Groups["token"].Value;

                        m = Regex.Match(req.Url.Query, "code=(?<code>.*)&scope=");
                        if (m.Success) accessToken = m.Groups["code"].Value;
                        // if (m.Success && m.Groups.ContainsKey("code")) accessToken = m.Groups["code"].Value;

                        m = Regex.Match(req.Url.Query, "error=(?<reason>.*)&error_description");
                        errString = m.Groups["reason"].Value;

                        data = Encoding.UTF8.GetBytes(tokenSuccess);
                        runServer = false;
                    } else {
                        data = Encoding.UTF8.GetBytes(tokenRedirect);
                    }
                } else {
                    string disableSubmit = !runServer ? "disabled" : "";
                    data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                }

                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                    pageViews += 1;

                // Write the response info
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }


        public static Task<string> Start(string[] args) {
            return Task.Run<string>(() => {
                // Create a Http server and start listening for incoming connections
                listener = new HttpListener();
                listener.Prefixes.Add(url);
                listener.Start();
                Logger.Trace("Listening for connections on {0}", url);

                // Handle requests
                Task listenTask = HandleIncomingConnections();
                listenTask.GetAwaiter().GetResult();

                // Close the listener
                listener.Close();
                return (accessToken == null ? "" : accessToken);
            });
        }
    }
}
