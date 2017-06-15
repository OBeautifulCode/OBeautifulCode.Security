﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CertHelper.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Security source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Naos.Recipes.TupleInitializers;

    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.Pkcs;
    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Cms;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Generators;
    using Org.BouncyCastle.Crypto.Operators;
    using Org.BouncyCastle.OpenSsl;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.Security;
    using Org.BouncyCastle.X509;
    using Org.BouncyCastle.X509.Extension;

    using Spritely.Recipes;

    /// <summary>
    /// Provides helpers methods for dealing with certificates.
    /// </summary>
#if !OBeautifulCodeSecurityRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Security", "See package version number")]
#endif
    internal static class CertHelper
    {
        /// <summary>
        /// Creates an RSA asymmetric cipher key pair.
        /// </summary>
        /// <param name="rsaKeyLength">The length of the rsa key (e.g. 2048 bits)</param>
        /// <returns>
        /// A RSA asymmetric cipher key pair.
        /// </returns>
        public static AsymmetricCipherKeyPair CreateRsaKeyPair(
            int rsaKeyLength = 2048)
        {
            var rsaKeyPairGenerator = new RsaKeyPairGenerator();
            rsaKeyPairGenerator.Init(new KeyGenerationParameters(new SecureRandom(), rsaKeyLength));
            var keyPair = rsaKeyPairGenerator.GenerateKeyPair();

            return keyPair;
        }

        /// <summary>
        /// Creates a certificate signing request for an SSL certificate.
        /// </summary>
        /// <remarks>
        /// Adapted from: <a href="https://gist.github.com/Venomed/5337717aadfb61b09e58" />
        /// </remarks>
        /// <param name="asymmetricKeyPair">The asymmetric cipher key pair.</param>
        /// <param name="commonName">The common name (e.g. "example.com").</param>
        /// <param name="subjectAlternativeNames">Optional.  The subject alternative names. (e.g. "shopping.example.com", "mail.example.com").</param>
        /// <param name="organizationalUnit">The organizational unit (e.g. "Engineering Dept").</param>
        /// <param name="organization">The organization (e.g. "The Example Company").</param>
        /// <param name="locality">The locality (e.g. "Seattle").</param>
        /// <param name="state">The state (e.g. "Washington").</param>
        /// <param name="country">The country (e.g. "US").</param>
        /// <returns>
        /// The certificate signing request.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="asymmetricKeyPair"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="commonName"/> or <paramref name="organizationalUnit"/>or <paramref name="organization"/> or <paramref name="locality"/> or <paramref name="state"/> or <paramref name="country"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="commonName"/> or <paramref name="organizationalUnit"/> or <paramref name="organization"/> or <paramref name="locality"/> or <paramref name="state"/> or <paramref name="country"/> is white space.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are many types required to construct a CSR.")]
        public static Pkcs10CertificationRequest CreateSslCsr(
            this AsymmetricCipherKeyPair asymmetricKeyPair,
            string commonName,
            IReadOnlyCollection<string> subjectAlternativeNames,
            string organizationalUnit,
            string organization,
            string locality,
            string state,
            string country)
        {
            new { asymmetricKeyPair }.Must().NotBeNull().OrThrow();
            new { commonName }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            new { organizationalUnit }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            new { organization }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            new { locality }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            new { state }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            new { country }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            var attributesInOrder = new List<Tuple<DerObjectIdentifier, string>>
            {
                { X509Name.C, country },
                { X509Name.ST, state },
                { X509Name.L, locality },
                { X509Name.O, organization },
                { X509Name.OU, organizationalUnit },
                { X509Name.CN, commonName }
            };

            var extensions = new Dictionary<DerObjectIdentifier, X509Extension>
            {
                { X509Extensions.BasicConstraints, new X509Extension(true, new DerOctetString(new BasicConstraints(false))) },
                { X509Extensions.KeyUsage, new X509Extension(true, new DerOctetString(new KeyUsage(KeyUsage.DigitalSignature | KeyUsage.KeyEncipherment | KeyUsage.DataEncipherment | KeyUsage.NonRepudiation))) },
                { X509Extensions.ExtendedKeyUsage, new X509Extension(false, new DerOctetString(new ExtendedKeyUsage(KeyPurposeID.IdKPServerAuth, KeyPurposeID.IdKPClientAuth))) },
                { X509Extensions.SubjectKeyIdentifier, new X509Extension(false, new DerOctetString(new SubjectKeyIdentifierStructure(asymmetricKeyPair.Public))) }
            };

            if ((subjectAlternativeNames != null) && subjectAlternativeNames.Any())
            {
                var generalNames = subjectAlternativeNames.Select(_ => new GeneralName(GeneralName.DnsName, _)).ToArray();
                extensions.Add(X509Extensions.SubjectAlternativeName, new X509Extension(false, new DerOctetString(new GeneralNames(generalNames))));
            }

            var result = CreateCsr(asymmetricKeyPair, SignatureAlgorithm.Sha1WithRsaEncryption, attributesInOrder, extensions);
            return result;
        }

        /// <summary>
        /// Creates a certificate signing request.
        /// </summary>
        /// <remarks>
        /// Adapted from: <a href="https://gist.github.com/Venomed/5337717aadfb61b09e58" />
        /// Adapted from: <a href="http://perfectresolution.com/2011/10/dynamically-creating-a-csr-private-key-in-net/" />
        /// </remarks>
        /// <param name="asymmetricKeyPair">The asymmetric cipher key pair.</param>
        /// <param name="signatureAlgorithm">The algorithm to use for signing.</param>
        /// <param name="attributesInOrder">
        /// The attributes to use in the subject in the order they should be scanned -
        /// from most general (e.g. country) to most specific.
        /// </param>
        /// <param name="extensions">The x509 extensions to apply.</param>
        /// <returns>
        /// A certificate signing request.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="asymmetricKeyPair"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="signatureAlgorithm"/> is <see cref="SignatureAlgorithm.None"/>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="attributesInOrder"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="attributesInOrder"/> is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="extensions"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="extensions"/> is empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are many types required to construct a CSR.")]
        public static Pkcs10CertificationRequest CreateCsr(
            this AsymmetricCipherKeyPair asymmetricKeyPair,
            SignatureAlgorithm signatureAlgorithm,
            IReadOnlyList<Tuple<DerObjectIdentifier, string>> attributesInOrder,
            IReadOnlyDictionary<DerObjectIdentifier, X509Extension> extensions)
        {
            new { asymmetricKeyPair }.Must().NotBeNull().OrThrow();
            new { signatureAlgorithm }.Must().NotBeEqualTo(SignatureAlgorithm.None).OrThrow();
            new { attributesInOrder }.Must().NotBeNull().And().NotBeEmptyEnumerable<Tuple<DerObjectIdentifier, string>>().OrThrowFirstFailure();
            new { extensions }.Must().NotBeNull().And().NotBeEmptyEnumerable<KeyValuePair<DerObjectIdentifier, X509Extension>>().OrThrowFirstFailure();

            var signatureFactory = new Asn1SignatureFactory(signatureAlgorithm.ToSignatureAlgorithmString(), asymmetricKeyPair.Private);

            var subject = new X509Name(attributesInOrder.Select(_ => _.Item1).ToList(), attributesInOrder.Select(_ => _.Item2).ToList());

            var extensionsForCsr = extensions.ToDictionary(_ => _.Key, _ => _.Value);

            var result = new Pkcs10CertificationRequest(
                signatureFactory,
                subject,
                asymmetricKeyPair.Public,
                new DerSet(new AttributePkcs(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(new X509Extensions(extensionsForCsr)))),
                asymmetricKeyPair.Private);
            return result;
        }

        /// <summary>
        /// Extracts a certificate chain from PKCS#7 CMS payload encoded in PEM.
        /// </summary>
        /// <param name="pemEncodedPayload">The payload containing the PKCS#7 CMS data.</param>
        /// <remarks>
        /// The method is expecting a PKCS#7/CMS SignedData structure containing no "content" and zero SignerInfos.
        /// </remarks>
        /// <returns>
        /// The certificate chain contained in the specified payload.
        /// </returns>
        public static IList<X509Certificate> ExtractCertChainFromPemEncodedPkcs7Cms(
            string pemEncodedPayload)
        {
            new { pemEncodedPayload }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            IList<X509Certificate> result;
            using (var stringReader = new StringReader(pemEncodedPayload))
            {
                var pemReader = new PemReader(stringReader);
                var pemObject = pemReader.ReadPemObject();
                var data = new CmsSignedData(pemObject.Content);
                var certStore = data.GetCertificates("COLLECTION");
                result = certStore.GetMatches(null).Cast<X509Certificate>().ToList();
            }

            return result;
        }

        private static string ToSignatureAlgorithmString(
            this SignatureAlgorithm signatureAlgorithm)
        {
            switch (signatureAlgorithm)
            {
                case SignatureAlgorithm.Md2WithRsaEncryption:
                    return "MD2WITHRSAENCRYPTION";
                case SignatureAlgorithm.Md5WithRsaEncryption:
                    return "MD5WITHRSAENCRYPTION";
                case SignatureAlgorithm.Sha1WithRsaEncryption:
                    return "SHA1WITHRSAENCRYPTION";
                case SignatureAlgorithm.Sha224WithRsaEncryption:
                    return "SHA224WITHRSAENCRYPTION";
                case SignatureAlgorithm.Sha256WithRsaEncryption:
                    return "SHA256WITHRSAENCRYPTION";
                case SignatureAlgorithm.Sha384WithRsaEncryption:
                    return "SHA384WITHRSAENCRYPTION";
                case SignatureAlgorithm.Sha512WithRsaEncryption:
                    return "SHA512WITHRSAENCRYPTION";
                case SignatureAlgorithm.IdRsassaPss:
                    return "SHA1WITHRSAANDMGF1";
                case SignatureAlgorithm.RsaSignatureWithRipeMd160:
                    return "RIPEMD160WITHRSAENCRYPTION";
                case SignatureAlgorithm.RsaSignatureWithRipeMd128:
                    return "RIPEMD128WITHRSAENCRYPTION";
                case SignatureAlgorithm.RsaSignatureWithRipeMd256:
                    return "RIPEMD256WITHRSAENCRYPTION";
                case SignatureAlgorithm.IdDsaWithSha1:
                    return "SHA1WITHDSA";
                case SignatureAlgorithm.DsaWithSha224:
                    return "SHA224WITHDSA";
                case SignatureAlgorithm.DsaWithSha256:
                    return "SHA256WITHDSA";
                case SignatureAlgorithm.DsaWithSha384:
                    return "SHA384WITHDSA";
                case SignatureAlgorithm.DsaWithSha512:
                    return "SHA512WITHDSA";
                case SignatureAlgorithm.EcDsaWithSha1:
                    return "SHA1WITHECDSA";
                case SignatureAlgorithm.EcDsaWithSha224:
                    return "SHA224WITHECDSA";
                case SignatureAlgorithm.EcDsaWithSha256:
                    return "SHA256WITHECDSA";
                case SignatureAlgorithm.EcDsaWithSha384:
                    return "SHA384WITHECDSA";
                case SignatureAlgorithm.EcDsaWithSha512:
                    return "SHA512WITHECDSA";
                case SignatureAlgorithm.GostR3411x94WithGostR3410x94:
                    return "GOST3411WITHGOST3410";
                case SignatureAlgorithm.GostR3411x94WithGostR3410x2001:
                    return "GOST3411WITHECGOST3410";
                default:
                    throw new NotSupportedException("this algorithm is not supported: " + signatureAlgorithm);
            }
        }
    }
}
