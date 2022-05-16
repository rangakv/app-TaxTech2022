using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTS.Security
{
    public class AzAppRegistration
    {
        public string Instance { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string clientSecret { get; set; }
    }
}

