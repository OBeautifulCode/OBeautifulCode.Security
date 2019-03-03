// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CertificateCreate.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    using System.Collections.Generic;

    /// <summary>
    /// Request to create a pending order for a certificate.
    /// </summary>
    public class CertificateCreate
    {
        /// <summary>
        /// Gets or sets the callback URL.
        /// </summary>
        /// <remarks>
        /// Required if client would like to receive stateful actions via callback during certificate lifecycle.
        /// </remarks>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the name to be secured in certificate. If provided, CN field in <see cref="Csr"/> will be ignored.
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Gets or sets contact information of the requestor.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public CertificateContact Contact { get; set; }

        /// <summary>
        /// Gets or sets the certificate signing request.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string Csr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use Intel V-Pro.
        /// </summary>
        /// <remarks>
        /// Only used for OV.
        /// </remarks>
        public bool IntelVPro { get; set; }

        /// <summary>
        /// Gets or sets the organization associated with the certificate being created.
        /// </summary>
        public CertificateOrganizationCreate Organization { get; set; }

        /// <summary>
        /// Gets or sets the number of years for certificate validity period.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public int Period { get; set; }

        /// <summary>
        /// Gets or sets the type of product requesting a certificate.
        /// </summary>
        /// <remarks>
        /// Required.  One of: DV_SSL, DV_WILDCARD_SSL, EV_SSL, OV_CS, OV_DS, OV_SSL, OV_WILDCARD_SSL, UCC_DV_SSL, UCC_EV_SSL, UCC_OV_SSL.
        /// </remarks>
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the root type.
        /// </summary>
        /// <remarks>
        /// One of: GODADDY_SHA_1, GODADDY_SHA_2, STARFIELD_SHA_1, STARFIELD_SHA_2
        /// Depending on certificate expiration date, SHA_1 not be allowed. Will default to SHA_2 if expiration date exceeds sha1 allowed date.
        /// </remarks>
        public string RootType { get; set; }

        /// <summary>
        /// Gets or sets the number of subject alternative names(SAN) to be included in certificate.
        /// </summary>
        /// <remarks>
        /// One of: FIVE, TEN, FIFTEEN, TWENTY, THIRTY, FOURTY, FIFTY, ONE_HUNDRED.
        /// </remarks>
        public string SlotSize { get; set; }

        /// <summary>
        /// Gets or sets a collection of Subject Alternative names to be included in certificate.
        /// </summary>
        public IReadOnlyCollection<string> SubjectAlternativeNames { get; set; }
    }
}
