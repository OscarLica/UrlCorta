using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETChallenge.DTO
{
    public class UrlAPIDTO
    {
        public string type { get; set; }
        public int id { get; set; }
        public attibute attributes { get; set; }
        public relationships relationships { get; set; }
    }

    public class attibute
    {
        public DateTime createat { get; set; }
        public string originalUrl { get; set; }
        public string url { get; set; }
        public int clicks { get; set; }
        
    }

    public class relationships {
        public clicks clicks { get; set; }
    }
    public class clicks {
        public List<clickData> data { get; set; }
    }

    public class clickData {
        public int id { get; set; }
        public string type { get; set; }
    }
}
