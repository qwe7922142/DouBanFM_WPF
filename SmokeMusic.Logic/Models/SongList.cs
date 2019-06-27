using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    [DataContract]
    public class SongList
    {
        [DataMember(Name = "r")]
        public int R { get; set; }
        [DataMember(Name = "song")]
        public List<Song> Songs { get; set; }
    }
}
