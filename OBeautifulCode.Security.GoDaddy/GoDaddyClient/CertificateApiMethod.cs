// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CertificateApiMethod.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    /// <summary>
    /// Specifies the GoDaddy Certificate API method.
    /// </summary>
    public enum CertificateApiMethod
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        Unknown,

        /// <summary>
        /// Create a pending order for a certificate.
        /// </summary>
        CreatePendingOrderForCertificate,

        /// <summary>
        /// Validate a pending order for a certificate.
        /// </summary>
        ValidatePendingOrderForCertificate,

        /// <summary>
        /// Retrieve certificate details.
        /// </summary>
        RetrieveCertificateDetails,

        /// <summary>
        /// Retrieve all certificate actions.
        /// </summary>
        RetrieveAllCertificateActions,

        /// <summary>
        /// Cancel a pending certificate.
        /// </summary>
        CancelPendingCertificate,

        /// <summary>
        /// Download a certificate.
        /// </summary>
        DownloadCertificate,

        /// <summary>
        /// Re-issue an active certificate.
        /// </summary>
        ReissueActiveCertificate,

        /// <summary>
        /// Renew an active certificate.
        /// </summary>
        RenewActiveCertificate,

        /// <summary>
        /// Revoke an active certificate.
        /// </summary>
        RevokeActiveCertificate,

        /// <summary>
        /// Check domain control.
        /// </summary>
        CheckDomainControl,
    }
}
