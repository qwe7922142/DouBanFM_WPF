using System;
using System.Runtime.Serialization;

namespace DouBanAPI.Models
{
    [DataContract]
    public class PlaySourceInfo
    {

        [DataMember(Name = "confidence")]
        public float confidence { get; set; }

        [DataMember(Name = "source_full_name")]
        public String source_full_name { get; set; }

        [DataMember(Name = "file_url")]
        public Uri file_url { get; set; }

        [DataMember(Name = "source")]
        public String source { get; set; }

        [DataMember(Name = "source_id")]
        public String source_id { get; set; }

        [DataMember(Name = "playable")]
        public bool playable { get; set; }

        [DataMember(Name = "page_url")]
        public Uri page_url { get; set; }
    }
}