﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AwsCertificateManagerPayload.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Security source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Recipes
{
    using Spritely.Recipes;

    /// <summary>
    /// Container for payload require to load certificates into the AWS Certificate Manager.
    /// </summary>
    public class AwsCertificateManagerPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwsCertificateManagerPayload"/> class.
        /// </summary>
        /// <param name="certificateBody">The PEM-encoded end-user certificate.</param>
        /// <param name="certificatePrivateKey">The PEM-encoded private key.</param>
        /// <param name="certificateChain">The PEM-encoded intermediate certificate chain.</param>
        public AwsCertificateManagerPayload(string certificateBody, string certificatePrivateKey, string certificateChain)
        {
            new { certificateBody }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            new { certificatePrivateKey }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            new { certificateChain }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            this.CertificateBody = certificateBody;
            this.CertificatePrivateKey = certificatePrivateKey;
            this.CertificateChain = certificateChain;
        }

        /// <summary>
        /// Gets the PEM-encoded certificate.
        /// </summary>
        public string CertificateBody { get; }

        /// <summary>
        /// Gets the PEM-encoded private key in RSA-1024 or RSA-2048.
        /// </summary>
        public string CertificatePrivateKey { get; }

        /// <summary>
        /// Gets the PEM-encoded intermediate certificate chain.
        /// </summary>
        public string CertificateChain { get; }
    }
}