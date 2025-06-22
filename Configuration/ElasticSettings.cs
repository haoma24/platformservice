using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Configuration
{
    public class ElasticSettings
    {
        public string Url { get; set; }
        public string DefaultIndex { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}