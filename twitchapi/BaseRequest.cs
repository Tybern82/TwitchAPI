#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using TwitchAPI.twitchapi.auth;

namespace TwitchAPI.twitchapi {

    public enum RequestMode { POST, GET }

    public abstract class BaseRequest<ResponseType> {

        public RequestMode Mode { get; protected set; } = RequestMode.POST;

        public string RequestURL { get; private set; }

        public TwitchScope RequiredScopes { get; private set; }

        public Dictionary<string, List<string>> QueryParameters { get; } = new Dictionary<string, List<string>>();
        public Dictionary<string, object> BodyParameters { get; } = new Dictionary<string, object>();

        public BaseRequest(string requestURL, TwitchScope requiredScopes) {
            RequestURL = requestURL;
            RequiredScopes = requiredScopes;
        }

        protected abstract void loadRequest();
        protected abstract ResponseType parseResponse(JObject data);

        public ResponseType doRequest() {
            return doRequest(new OAuthClientCredentialsRequest());
        }

        public ResponseType doRequest(OAuthBaseRequest req) {
            loadRequest();
            UriBuilder requestURL = new UriBuilder(RequestURL);
            foreach (KeyValuePair<string,List<string>> param in QueryParameters) {
                foreach (string s in param.Value) {
                    AddQueryParameter(requestURL, param.Key, s);
                }
            }
            WebRequest request = WebRequest.Create(requestURL.Uri);
            switch (Mode) {
                case RequestMode.POST:
                    request.Method = "POST";
                    break;

                case RequestMode.GET:
                default:
                    request.Method = "GET";
                    break;
            }

            // Add Authentication
            req.Scope |= RequiredScopes;
            request.Headers.Add("Client-Id: " + req.ClientID);
            request.Headers.Add("Authorization: Bearer " + req.CommandAccessToken);

            if (Mode == RequestMode.POST) {
                // Do POST
                request.ContentType = "application/json";
                JObject body = new JObject();
                foreach (KeyValuePair<string, object> param in BodyParameters) {
                    body[param.Key] = JToken.FromObject(param.Value);
                }
                byte[] data = Encoding.UTF8.GetBytes(body.ToString());
                request.ContentLength = data.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                // Close the Stream object.
                dataStream.Close();
            }

            WebResponse response = request.GetResponse();
            string serverResponse = (new StreamReader(response.GetResponseStream())).ReadToEnd();
            response.Close();
            return parseResponse(JObject.Parse(serverResponse));
        }

        private static void AddQueryParameter(UriBuilder baseUri, string key, string value) {
            string queryToAppend = Uri.EscapeUriString(key) + "=" + Uri.EscapeUriString(value);

            if (baseUri.Query != null && baseUri.Query.Length > 1)
                // Note: In .NET Core and .NET 5+, you can simplify by removing
                // the call to Substring(), which removes the leading "?" character.
                baseUri.Query = baseUri.Query.Substring(1) + "&" + queryToAppend;
            else
                baseUri.Query = queryToAppend;
        }
    }
}
