// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CertificateAddress.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    /// <summary>
    /// Gets or sets an address.
    /// </summary>
    public class CertificateAddress
    {
        /// <summary>
        /// Gets or sets the address line 1 of the organization's address.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address line 2 of the organization's address.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city/locality of the organization's address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the two character county code of the organization.
        /// </summary>
        /// <remarks>
        /// Required.  One of the following: AC, AD, AE, AF, AG, AI, AL, AM, AN, AO, AQ, AR, AS, AT, AU, AW, AZ, BA, BB, BD, BE, BF, BG, BH, BI, BJ, BM, BN, BO, BR, BS, BT, BV, BW, BY, BZ, CA, CC, CD, CF, CG, CH, CI, CK, CL, CM, CN, CO, CR, CV, CX, CY, CZ, DE, DJ, DK, DM, DO, DZ, EC, EE, EG, EH, ER, ES, ET, FI, FJ, FK, FM, FO, FR, GA, GB, GD, GE, GF, GG, GH, GI, GL, GM, GN, GP, GQ, GR, GS, GT, GU, GW, GY, HK, HM, HN, HR, HT, HU, ID, IE, IL, IM, IN, IO, IQ, IS, IT, JE, JM, JO, JP, KE, KG, KH, KI, KM, KN, KR, KW, KY, KZ, LA, LB, LC, LI, LK, LR, LS, LT, LU, LV, LY, MA, MC, MD, ME, MG, MH, ML, MM, MN, MO, MP, MQ, MR, MS, MT, MU, MV, MW, MX, MY, MZ, NA, NC, NE, NF, NG, NI, NL, NO, NP, NR, NU, NZ, OM, PA, PE, PF, PG, PH, PK, PL, PM, PN, PR, PS, PT, PW, PY, QA, RE, RO, RS, RU, RW, SA, SB, SC, SE, SG, SH, SI, SJ, SK, SL, SM, SN, SO, SR, ST, SV, SZ, TC, TD, TF, TG, TH, TJ, TK, TL, TM, TN, TO, TP, TR, TT, TV, TW, TZ, UA, UG, UM, US, UY, UZ, VA, VC, VE, VG, VI, VN, VU, WF, WS, YE, YT, YU, ZA, ZM, ZW.
        /// </remarks>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the organization's address.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the full name of the state/province/territory of the organization's address.
        /// </summary>
        public string State { get; set; }
    }
}
