using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouBanAPI.Models
{
    public  class CoverInfo
    {
        public bool IsLarge { get; set; }
        public bool IsMedium { get; set; }

        public bool IsDefault { get; set; }

        public Uri Uri { get; set; }

        public CoverInfo(Uri uri )
        {
            Uri = uri;
            if (uri.ToString().Contains("large"))
            {
                IsLarge = true;
                return;
            }
            if (uri.ToString().Contains("medium"))
            {
                IsMedium = true;
                return;
            }
            IsDefault = true;
        }
    }
}
