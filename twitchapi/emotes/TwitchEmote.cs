using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchAPI.twitchapi.emotes {
    public class TwitchEmote {

        public int ID { get; private set; }
        public string Regex { get; private set; }
        public TwitchEmoteImage Images { get; private set; }

        public TwitchEmote(JToken jsonData) {
            ID = jsonData.Value<int>("id");
            Regex = jsonData.Value<string>("regex") ?? "";
            JToken? images = jsonData.Value<JToken>("images");
            Images = (images != null) ? new TwitchEmoteImage(images) : new TwitchEmoteImage();
        }
    }

    public class TwitchEmoteImage {

        public int EmoticonSet { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public string URL { get; private set; }

        public TwitchEmoteImage(JToken jsonData) {
            EmoticonSet = jsonData.Value<int>("emoticon_set");
            Height = jsonData.Value<int>("height");
            Width = jsonData.Value<int>("Width");
            URL = jsonData.Value<string>("url") ?? "";
        }

        public TwitchEmoteImage() {
            EmoticonSet = 0;
            Height = 0;
            Width = 0;
            URL = "";
        }
    }
}

/*
       {
         "id": 25,
         "regex": "Kappa",
         "images": {
            "emoticon_set": 0,
            "height": 28,
            "width": 25,
            "url": "https://static-cdn.jtvnw.net/emoticons/v1/25/1.0"
         }
      }
*/