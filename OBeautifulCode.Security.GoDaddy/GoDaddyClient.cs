// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GoDaddyClient.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Represents a client that can communicate with GoDaddy.
    /// </summary>
    public class GoDaddyClient
    {
        private static readonly Dictionary<GoDaddyEnvironment, Uri> EnvironmentToBaseUriMap =
            new Dictionary<GoDaddyEnvironment, Uri>
            {
                { GoDaddyEnvironment.OperatingTestEnvironment, new Uri("https://api.ote-godaddy.com/api/v1/") },
                { GoDaddyEnvironment.Production, new Uri("https://api.godaddy.com/api/v1/") },
            };

        private readonly string accessKey;

        private readonly string secretKey;

        private readonly Uri baseUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoDaddyClient"/> class.
        /// </summary>
        /// <param name="accessKey">The API access key.</param>
        /// <param name="secretKey">The API secret key.</param>
        /// <param name="environment">The GoDaddy environment to connect to.</param>
        public GoDaddyClient(
            string accessKey,
            string secretKey,
            GoDaddyEnvironment environment)
        {
            new { accessKey }.Must().NotBeNullNorWhiteSpace();
            new { secretKey }.Must().NotBeNullNorWhiteSpace();
            new { environment }.Must().NotBeEqualTo(GoDaddyEnvironment.Unknown);

            this.accessKey = accessKey;
            this.secretKey = secretKey;
            this.baseUri = EnvironmentToBaseUriMap[environment];
        }

        /// <summary>
        /// Create a pending order for certificate.
        /// </summary>
        /// <param name="request">The certificate order information.</param>
        /// <param name="marketId">Optional setting locale for communications such as emails and error messages.  Default is the default locale for the shopper account.</param>
        /// <returns>
        /// The certificate identifier.
        /// </returns>
        public async Task<CertificateIdentifierResponse> CreatePendingOrderForCertificate(
            CertificateCreate request,
            string marketId = null)
        {
            var client = this.GetHttpClient();

            if (marketId != null)
            {
                client.DefaultRequestHeaders.Add("X-Market-Id", marketId);
            }

            var response = await client.PostAsync("certificates", request);

            await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.CreatePendingOrderForCertificate);

            var result = await response.Content.DeserializeAsync<CertificateIdentifierResponse>();

            return result;
        }

        ///// <summary>
        ///// Validate a pending order for a certificate.
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="XMarketId"></param>
        ///// <returns></returns>
        // public async Task<CertificateIdentifierResponse> ValidatePendingOrderForCertificate()
        // {
        //    await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.ValidatePendingOrderForCertificate);
        //    return null;
        // }
        ///// <summary>
        ///// Retrieve certificate details
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        // public async Task<CertificateIdentifierResponse> RetrieveCertificateDetails(CertificateDetailRetrieve request)
        // {
        //    var client = this.GetHttpClient();
        //    var response = await client.GetAsync($"certificates/{request.certificateId}");
        //    await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.RetrieveCertificateDetails);
        //    return await response.Content.ReadAsAsync<CertificateIdentifierResponse>();
        // }
        ///// <summary>
        ///// Retrieve certificate detail
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        // public async Task<List<CertificateActionResponse>> RetrieveAllCertificateActions(CertificateActionRetrieve request)
        // {
        //    var client = this.GetHttpClient();
        //    var response = await client.GetAsync($"certificates/{request.certificateId}/actions");
        //    await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.RetrieveAllCertificateActions);
        //    return await response.Content.ReadAsAsync<List<CertificateActionResponse>>();
        // }
        ///// <summary>
        ///// Cancel a pending certificate
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        // public async Task CancelPendingCertificate(CertificateCancel request)
        // {
        //    var client = this.GetHttpClient();
        //    var response = await client.PostAsync($"certificates/{request.certificateId}/cancel", request);
        //    await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.CancelPendingCertificate);
        //    return;
        // }
        ///// <summary>
        ///// Download certificate
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        // public async Task<CertificateBundleResponse> DownloadCertificate(CertificateDownload request)
        // {
        //    var client = this.GetHttpClient();
        //    var response = await client.GetAsync($"certificates/{request.certificateId}/download");
        //    await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.DownloadCertificate);
        //    return await response.Content.ReadAsAsync<CertificateBundleResponse>();
        // }
        ///// <summary>
        ///// Reissue active certificate
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="certificateId"></param>
        ///// <returns></returns>
        // public async Task ReissueActiveCertificate(CertificateReissue request, string certificateId)
        // {
        //    if (request.delayExistingRevoke != null)
        //    {
        //        new { request.delayExistingRevoke }.Must().BeInRange(0, 168);
        //    }
        //    var client = this.GetHttpClient();
        //    var response = await client.PostAsync($"certificates/{certificateId}/reissue", request);
        //    await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.ReissueActiveCertificate);
        //    return;
        // }
        ///// <summary>
        ///// Renew active certificate
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="certificateId"></param>
        ///// <returns></returns>
        // public async Task RenewActiveCertificate(CertificateRenew request, string certificateId)
        // {
        //    var client = this.GetHttpClient();
        //    var response = await client.PostAsync($"certificates/{certificateId}/renew", request);
        //    await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.RenewActiveCertificate);
        //    return;
        // }
        ///// <summary>
        ///// Revoke active certificate
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="certificateId"></param>
        ///// <returns></returns>
        // public async Task RevokeActiveCertificate(CertificateRevoke request, string certificateId)
        // {
        //    var client = this.GetHttpClient();
        //    var response = await client.PostAsync($"certificates/{certificateId}/revoke", request);
        //    await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.RevokeActiveCertificate);
        //    return;
        // }
        ///// <summary>
        ///// Check Domain Control
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        // public async Task CheckDomainControl(CertificateDomainControlCheck request)
        // {
        //    var client = this.GetHttpClient();
        //    var response = await client.PostAsync($"certificates/{request.certificateId}/verifydomaincontrol", null);
        //    await ThrowIfResponseIsNotSuccessful(response, CertificateApiMethod.CheckDomainControl);
        //    return;
        // }
        private static async Task ThrowIfResponseIsNotSuccessful(
            HttpResponseMessage response,
            CertificateApiMethod method)
        {
            var actualStatusCode = (int)response.StatusCode;
            var expectedStatusCode = ResponseStatusCodeMaps.MethodToSuccessfulStatusCodeMap[method];

            if (actualStatusCode != expectedStatusCode)
            {
                var statusCodeToFailureKindMap = ResponseStatusCodeMaps.MethodToStatusCodeToFailureKindMap[method];
                statusCodeToFailureKindMap.TryGetValue(actualStatusCode, out var failureKind);

                var contentJson = await response.Content.ReadAsStringAsync();

                throw new GoDaddyApiException(method, actualStatusCode, failureKind, contentJson);
            }
        }

        private HttpClient GetHttpClient()
        {
            var result = new HttpClient { BaseAddress = this.baseUri };

            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            result.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("sso-key", $"{this.accessKey}:{this.secretKey}");

            return result;
        }
    }
}
