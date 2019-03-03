// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResponseStatusCodeMaps.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    using System.Collections.Generic;

    /// <summary>
    /// Maps response status codes to their interpretations.
    /// </summary>
    internal static class ResponseStatusCodeMaps
    {
#pragma warning disable SA1401 // Fields should be private

        /// <summary>
        /// A map of response status code to the kind of failure that that
        /// code indicates, indexed by the GoDaddy Certificate API method.
        /// </summary>
        public static IReadOnlyDictionary<CertificateApiMethod, IReadOnlyDictionary<int, CertificateApiErrorKind>>
            MethodToStatusCodeToFailureKindMap =
                new Dictionary<CertificateApiMethod, IReadOnlyDictionary<int, CertificateApiErrorKind>>
                {
                    {
                        CertificateApiMethod.CreatePendingOrderForCertificate,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 400, CertificateApiErrorKind.RequestWasMalformed },
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 409, CertificateApiErrorKind.CertificateStateDoesNotAllowRenew },
                            { 422, CertificateApiErrorKind.EmailNotEmptyOrCsrIsInvalid },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                    {
                        CertificateApiMethod.ValidatePendingOrderForCertificate,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 400, CertificateApiErrorKind.RequestWasMalformed },
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 409, CertificateApiErrorKind.CertificateStateDoesNotAllowRenew },
                            { 422, CertificateApiErrorKind.EmailNotEmptyOrCsrIsInvalid },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                    {
                        CertificateApiMethod.RetrieveCertificateDetails,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 400, CertificateApiErrorKind.RequestWasMalformed },
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 404, CertificateApiErrorKind.CertificateIdNotFound },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                    {
                        CertificateApiMethod.RetrieveAllCertificateActions,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 400, CertificateApiErrorKind.RequestWasMalformed },
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 404, CertificateApiErrorKind.CertificateIdNotFound },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                    {
                        CertificateApiMethod.CancelPendingCertificate,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 404, CertificateApiErrorKind.CertificateIdNotFound },
                            { 409, CertificateApiErrorKind.CertificateStateDoesNotAllowCancel },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                    {
                        CertificateApiMethod.DownloadCertificate,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 400, CertificateApiErrorKind.RequestWasMalformed },
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 404, CertificateApiErrorKind.CertificateIdNotFound },
                            { 409, CertificateApiErrorKind.CertificateStateDoesNotAllowDownload },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                    {
                        CertificateApiMethod.ReissueActiveCertificate,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 400, CertificateApiErrorKind.RequestWasMalformed },
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 404, CertificateApiErrorKind.CertificateIdNotFound },
                            { 409, CertificateApiErrorKind.CertificateStateDoesNotAllowReissue },
                            { 422, CertificateApiErrorKind.CsrIsInvalidOrDelayRevocationExceedsMaximum },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                    {
                        CertificateApiMethod.RenewActiveCertificate,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 400, CertificateApiErrorKind.RequestWasMalformed },
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 404, CertificateApiErrorKind.CertificateIdNotFound },
                            { 409, CertificateApiErrorKind.CertificateStateDoesNotAllowRenew },
                            { 422, CertificateApiErrorKind.CsrIsInvalid },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                    {
                        CertificateApiMethod.RenewActiveCertificate,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 400, CertificateApiErrorKind.RequestWasMalformed },
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 404, CertificateApiErrorKind.CertificateIdNotFound },
                            { 409, CertificateApiErrorKind.CertificateStateDoesNotAllowRevoke },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                    {
                        CertificateApiMethod.CheckDomainControl,
                        new Dictionary<int, CertificateApiErrorKind>
                        {
                            { 400, CertificateApiErrorKind.RequestWasMalformed },
                            { 401, CertificateApiErrorKind.AuthenticationInfoNotSentOrInvalid },
                            { 403, CertificateApiErrorKind.AuthenticatedUserIsNotAllowedAccess },
                            { 404, CertificateApiErrorKind.CertificateIdNotFound },
                            { 409, CertificateApiErrorKind.DomainControlWasNotSuccessfulOrCertificateStateDoesNotAllowDomainControl },
                            { 500, CertificateApiErrorKind.InternalServerError },
                        }
                    },
                };

        /// <summary>
        /// A map of GoDaddy Certificate API methods to the status code that indicates that
        /// that method was executed successfully.
        /// </summary>
        public static IReadOnlyDictionary<CertificateApiMethod, int> MethodToSuccessfulStatusCodeMap =
            new Dictionary<CertificateApiMethod, int>
            {
                { CertificateApiMethod.CreatePendingOrderForCertificate, 202 },
                { CertificateApiMethod.ValidatePendingOrderForCertificate, 204 },
                { CertificateApiMethod.RetrieveCertificateDetails, 200 },
                { CertificateApiMethod.RetrieveAllCertificateActions, 200 },
                { CertificateApiMethod.CancelPendingCertificate, 204 },
                { CertificateApiMethod.DownloadCertificate, 200 },
                { CertificateApiMethod.ReissueActiveCertificate, 202 },
                { CertificateApiMethod.RenewActiveCertificate, 202 },
                { CertificateApiMethod.RevokeActiveCertificate, 204 },
                { CertificateApiMethod.CheckDomainControl, 204 },
            };

#pragma warning restore SA1401 // Fields should be private
    }
}
