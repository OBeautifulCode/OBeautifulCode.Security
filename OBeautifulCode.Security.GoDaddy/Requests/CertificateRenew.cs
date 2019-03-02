using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBeautifulCode.Security.GoDaddy
{
    public class CertificateRenew
    {
        public List<string> subjectAlternativeNames { get; set; }
        public int period { get; set; }
        public string csr { get; set; }
        public string rootType { get; set; }
        public string callbackUrl { get; set; }
        public string commonName { get; set; }
    }
}
