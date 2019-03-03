// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CertificateApiErrorKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    /// <summary>
    /// Specifies the kind of error that occured when calling the GoDaddy
    /// Certificate API.  These are the GoDaddy documented errors; GoDaddy
    /// received the request the responded with an error.  As such these errors
    /// do not cover items like network connectivity issues where the API was
    /// never actually hit.
    /// </summary>
    /// <remarks>
    /// See <a href="https://developer.godaddy.com/doc/endpoint/certificates#/v1"/>.
    /// </remarks>
    public enum CertificateApiErrorKind
    {
        /// <summary>
        /// The error is not a known/documented error for the API method.
        /// </summary>
        Unknown,

        /// <summary>
        /// The request was malformed.
        /// </summary>
        RequestWasMalformed,

        /// <summary>
        /// Authentication info not sent or invalid.
        /// </summary>
        AuthenticationInfoNotSentOrInvalid,

        /// <summary>
        /// Authenticated user is not allowed access.
        /// </summary>
        AuthenticatedUserIsNotAllowedAccess,

        /// <summary>
        /// Certificate id not found.
        /// </summary>
        CertificateIdNotFound,

        /// <summary>
        /// Certificate state does not allow renew.
        /// </summary>
        CertificateStateDoesNotAllowRenew,

        /// <summary>
        /// Certificate state does not allow cancel.
        /// </summary>
        CertificateStateDoesNotAllowCancel,

        /// <summary>
        /// Certificate state does not allow download.
        /// </summary>
        CertificateStateDoesNotAllowDownload,

        /// <summary>
        /// Certificate state does not allow reissue.
        /// </summary>
        CertificateStateDoesNotAllowReissue,

        /// <summary>
        /// Certificate state does not allow revoke.
        /// </summary>
        CertificateStateDoesNotAllowRevoke,

        /// <summary>
        /// The CSR is invalid.
        /// </summary>
        CsrIsInvalid,

        /// <summary>
        /// Email not empty or CSR is invalid.
        /// </summary>
        EmailNotEmptyOrCsrIsInvalid,

        /// <summary>
        /// CSR is invalid or delay revocation exceeds maximum.
        /// </summary>
        CsrIsInvalidOrDelayRevocationExceedsMaximum,

        /// <summary>
        /// Domain control was not successful or certificate state does not allow domain control.
        /// </summary>
        DomainControlWasNotSuccessfulOrCertificateStateDoesNotAllowDomainControl,

        /// <summary>
        /// Internal server error.
        /// </summary>
        InternalServerError,
    }
}
