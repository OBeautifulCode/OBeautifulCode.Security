// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CertificateOrganizationCreate.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    /// <summary>
    /// The organization associated with the certificate being created.
    /// </summary>
    public class CertificateOrganizationCreate
    {
        /// <summary>
        /// Gets or sets the organization's address.
        /// </summary>
        public CertificateAddress Address { get; set; }

        /// <summary>
        /// Gets or sets the assumed name.
        /// </summary>
        /// <remarks>
        /// Only for EVSSL.  The DBA (does business as) name for the organization.
        /// </remarks>
        public string AssumedName { get; set; }

        /// <summary>
        /// Gets or sets the name of the organization that owns the common name.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number for the organization.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the registration agent.
        /// </summary>
        /// <remarks>
        /// Only for EVSSL.
        /// </remarks>
        public string RegistrationAgent { get; set; }

        /// <summary>
        /// Gets or sets the registration number.
        /// </summary>
        /// <remarks>
        /// Only for EVSSL.
        /// </remarks>
        public string RegistrationNumber { get; set; }
    }
}
