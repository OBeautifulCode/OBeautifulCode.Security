﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBeautifulCode.Security.GoDaddy
{
    public class PEMCertificatesResponse
    {
        public string Certificate { get; set; }
        public string Intermediate { get; set; }
        public string Root { get; set; }
        public string Cross { get; set; }
    }
}
