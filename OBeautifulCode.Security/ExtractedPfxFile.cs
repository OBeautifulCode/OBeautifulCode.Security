﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtractedPfxFile.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Security source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Recipes
{
    using System.Collections.Generic;

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.X509;

    using Spritely.Recipes;

    /// <summary>
    /// Represents cryptographic objects extracted from a PFX file.
    /// </summary>
    public class ExtractedPfxFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedPfxFile"/> class.
        /// </summary>
        /// <param name="certificateChain">The certificate chain, in no particular order.</param>
        /// <param name="privateKey">The private key.</param>
        public ExtractedPfxFile(
            IReadOnlyList<X509Certificate> certificateChain,
            AsymmetricKeyParameter privateKey)
        {
            new { certificateChain }.Must().NotBeNull().And().NotBeEmptyEnumerable<X509Certificate>().And().NotContainAnyNulls<X509Certificate>().OrThrowFirstFailure();
            new { privateKey }.Must().NotBeNull().OrThrow();

            CertificateChain = certificateChain;
            PrivateKey = privateKey;
        }

        /// <summary>
        /// Gets the certificate chain, in no particular order.
        /// </summary>
        public IReadOnlyList<X509Certificate> CertificateChain { get; }

        /// <summary>
        /// Gets the private key.
        /// </summary>
        public AsymmetricKeyParameter PrivateKey { get; }
    }
}
