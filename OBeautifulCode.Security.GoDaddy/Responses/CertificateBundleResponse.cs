using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBeautifulCode.Security.GoDaddy
{
    public class CertificateBundleResponse
    {
        public string SerialNumber { get; set; }
        public PEMCertificatesResponse Addresses { get; set; }
    }
}
