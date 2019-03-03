// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleGoDaddyClient.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    using System.Threading.Tasks;

    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// A <see cref="GoDaddyClient"/> wrapper that simplifies it's usage.
    /// </summary>
    public class SimpleGoDaddyClient
    {
        private readonly GoDaddyClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleGoDaddyClient"/> class.
        /// </summary>
        /// <param name="accessKey">The API access key.</param>
        /// <param name="secretKey">The API secret key.</param>
        /// <param name="environment">The GoDaddy environment to connect to.</param>
        public SimpleGoDaddyClient(
            string accessKey,
            string secretKey,
            GoDaddyEnvironment environment)
        {
            this.client = new GoDaddyClient(accessKey, secretKey, environment);
        }

        /// <summary>
        /// Create a new certificate.
        /// </summary>
        /// <param name="pemEncodedCsr">A PEM-encoded CSR.</param>
        /// <param name="numberOfYearsCertIsValidFor">The number of years that the certificate is valid for.</param>
        /// <param name="productKind">The kind of certificate product.</param>
        /// <returns>
        /// The certificate identifier.
        /// </returns>
        public async Task<string> CreateNewCertAsync(
            string pemEncodedCsr,
            int numberOfYearsCertIsValidFor,
            ProductKind productKind)
        {
            new { pemEncodedCsr }.Must().NotBeNullNorWhiteSpace();
            new { numberOfYearsCertIsValidFor }.Must().BeGreaterThanOrEqualTo(1).And().BeLessThanOrEqualTo(3);
            new { productKind }.Must().NotBeEqualTo(ProductKind.Unknown);

            var request = new CertificateCreate
            {
                Csr = pemEncodedCsr,
                Period = numberOfYearsCertIsValidFor,
                ProductType = productKind.ToApiCompatibleString(),
            };

            var response = await this.client.CreatePendingOrderForCertificateAsync(request);

            var result = response.CertificateId;

            return result;
        }
    }
}
