using NETChallenge.DTO;
using NETChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETChallenge.ViewModels
{
    public class ShowViewModel
    {
        public Url Url { get; set; }
        public List<MetricasDTO> DailyClicks { get; set; }
        public List<MetricasDTO> BrowseClicks { get; set; }
        public List<MetricasDTO> PlatformClicks { get; set; }
    }
}
