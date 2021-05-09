#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TwitchAPI.twitchapi.ads {

    public enum TwitchCommercialLength { TIME30, TIME60, TIME90, TIME120, TIME150, TIME180 }

    public class StartCommercialRequest : BaseRequest<StartCommercialResponse> {

        public string BroadcasterID { get; set; } = "";
        public TwitchCommercialLength Length { get; set; } = TwitchCommercialLength.TIME30;

        public StartCommercialRequest() : base("https://api.twitch.tv/helix/channels/commercial", auth.TwitchScope.CHANNEL_EDIT_COMMERCIAL) { }

        protected override void loadRequest() { 
            QueryParameters.Clear();
            BodyParameters.Clear();
            BodyParameters["broadcaster_id"] = BroadcasterID;
            switch (Length) {
                case TwitchCommercialLength.TIME180:    BodyParameters["length"] = 180; break;
                case TwitchCommercialLength.TIME150:    BodyParameters["length"] = 150; break;
                case TwitchCommercialLength.TIME120:    BodyParameters["length"] = 120; break;
                case TwitchCommercialLength.TIME90:     BodyParameters["length"] = 90; break;
                case TwitchCommercialLength.TIME60:     BodyParameters["length"] = 60; break;
                case TwitchCommercialLength.TIME30:
                default:                                BodyParameters["length"] = 30; break;
            }
        }

        protected override StartCommercialResponse parseResponse(JObject data) {
            return new StartCommercialResponse(data);
        }
    }

    public class StartCommercialResponse {
        public TimeSpan Length { get; private set; }
        public string Message { get; private set; }
        public TimeSpan RetryAfter { get; private set; }

        public StartCommercialResponse(JObject data) {
            JToken? currToken = data["data"];
            if (currToken == null) throw new TwitchAPIException("Missing data in response.");
            JArray dataElement = (JArray)currToken;
            if (dataElement.Count < 1) throw new TwitchAPIException("No body to data in response.");
            JToken dataBody = dataElement[0];
            int length = dataBody.Value<int>("length");
            int retryAfter = dataBody.Value<int>("retry_after");
            string? message = dataBody.Value<string>("message");

            Length = new TimeSpan(0, 0, length);
            RetryAfter = new TimeSpan(0, 0, retryAfter);
            Message = (message == null) ? "" : message;
        }
    }
}