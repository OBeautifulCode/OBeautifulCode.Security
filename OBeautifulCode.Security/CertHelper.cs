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
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "There are a lot of things you can do with certs!  Should really break this up...")]
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
        /// Creates a PFX file.
        /// </summary>
        /// <param name="certChain">The cert chain.  The order of the certificates is inconsequential.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="unsecurePassword">The password for the PFX file.</param>
        /// <param name="pfxFilePath">The path to write the PFX file to.</param>
        /// <param name="overwrite">
        /// Determines whether to overwrite a file that already exist at <paramref name="pfxFilePath"/>.
        /// If false and a file exists at that path, the method will throw.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="certChain"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="certChain"/> is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="privateKey"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="privateKey"/> is not private.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="unsecurePassword"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unsecurePassword"/> is white space.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="pfxFilePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="pfxFilePath"/> is white space.</exception>
        /// <exception cref="IOException"><paramref name="overwrite"/> is false and there is a file at <paramref name="pfxFilePath"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Creating a PFX requires lots of types.")]
        public static void CreatePfxFile(
            IReadOnlyList<X509Certificate> certChain,
            AsymmetricKeyParameter privateKey,
            string unsecurePassword,
            string pfxFilePath,
            bool overwrite)
        {
            new { certChain }.Must().NotBeNull().And().NotBeEmptyEnumerable<X509Certificate>().OrThrowFirstFailure();
            new { privateKey }.Must().NotBeNull().OrThrow();
            new { privateKey.IsPrivate }.Must().BeTrue().OrThrow();
            new { unsecurePassword }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            new { pfxFilePath }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            var mode = overwrite ? FileMode.Create : FileMode.CreateNew;
            using (var fileStream = new FileStream(pfxFilePath, mode, FileAccess.Write, FileShare.None))
            {
                CreatePfxFile(certChain, privateKey, unsecurePassword, fileStream);
            }
        }

        /// <summary>
        /// Creates a PFX file.
        /// </summary>
        /// <remarks>
        /// adapted from: <a href="https://boredwookie.net/blog/m/bouncy-castle-create-a-basic-certificate" />
        /// </remarks>
        /// <param name="certChain">The cert chain.  The order of the certificates is inconsequential.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="unsecurePassword">The password for the PFX file.</param>
        /// <param name="output">The stream to write the PFX file to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="certChain"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="certChain"/> is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="privateKey"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="privateKey"/> is not private.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="unsecurePassword"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unsecurePassword"/> is white space.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="output"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="output"/> is not writable.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Creating a PFX requires lots of types.")]
        public static void CreatePfxFile(
            IReadOnlyList<X509Certificate> certChain,
            AsymmetricKeyParameter privateKey,
            string unsecurePassword,
            Stream output)
        {
            new { certChain }.Must().NotBeNull().And().NotBeEmptyEnumerable<X509Certificate>().OrThrowFirstFailure();
            new { privateKey }.Must().NotBeNull().OrThrow();
            new { privateKey.IsPrivate }.Must().BeTrue().OrThrow();
            new { unsecurePassword }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrow();
            new { output }.Must().NotBeNull().OrThrow();
            new { output.CanWrite }.Must().BeTrue().OrThrow();

            certChain = certChain.OrderCertChainFromLowestToHighestLevelOfTrust();

            var store = new Pkcs12StoreBuilder().Build();
            var certEntries = new List<X509CertificateEntry>();
            foreach (var cert in certChain)
            {
                var certEntry = new X509CertificateEntry(cert);
                certEntries.Add(certEntry);
                store.SetCertificateEntry(cert.GetX509SubjectAttributes()[X509SubjectAttributeKind.CommonName], certEntry);
            }

            var keyEntry = new AsymmetricKeyEntry(privateKey);
            store.SetKeyEntry(certChain.First().GetX509SubjectAttributes()[X509SubjectAttributeKind.CommonName], keyEntry, certEntries.ToArray());
            store.Save(output, unsecurePassword.ToCharArray(), new SecureRandom());
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
        /// Extracts the certificate chain from a PFX file.
        /// </summary>
        /// <param name="input">A byte array of the PFX.</param>
        /// <param name="unsecurePassword">The PFX password in clear-text.</param>
        /// <returns>
        /// The certificate chain archived in the input PFX.  Certs are in no particular order.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="unsecurePassword"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unsecurePassword"/> is white space.</exception>
        public static IReadOnlyCollection<X509Certificate> ExtractCertChainFromPfx(
            byte[] input,
            string unsecurePassword)
        {
            new { input }.Must().NotBeNull().OrThrow();
            new { unsecurePassword }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            using (var inputStream = new MemoryStream(input))
            {
                var result = ExtractCertChainFromPfx(inputStream, unsecurePassword);
                return result;
            }
        }

        /// <summary>
        /// Extracts the certificate chain from a PFX file.
        /// </summary>
        /// <param name="input">A stream with the PFX.</param>
        /// <param name="unsecurePassword">The PFX password in clear-text.</param>
        /// <returns>
        /// The certificate chain archived in the input PFX.  Certs are in no particular order.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="input"/> is not readable.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="unsecurePassword"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unsecurePassword"/> is white space.</exception>
        public static IReadOnlyCollection<X509Certificate> ExtractCertChainFromPfx(
            Stream input,
            string unsecurePassword)
        {
            new { input }.Must().NotBeNull().OrThrow();
            new { input.CanRead }.Must().BeTrue().OrThrow();
            new { unsecurePassword }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            var store = new Pkcs12Store(input, unsecurePassword.ToCharArray());
            var aliases = store.Aliases;

            var result = new List<X509Certificate>();
            foreach (var alias in aliases)
            {
                var certEntry = store.GetCertificate(alias.ToString());
                result.Add(certEntry.Certificate);
            }

            return result;
        }

        /// <summary>
        /// Re-orders a certificate chain from lowest to highest level of trust.
        /// </summary>
        /// <param name="certChain">The certificate chain to re-order.</param>
        /// <returns>
        /// The certificates in the specified chain, ordered from lowest to highest level of trust.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="certChain"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="certChain"/> is empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="certChain"/> is malformed.</exception>
        public static IReadOnlyList<X509Certificate> OrderCertChainFromLowestToHighestLevelOfTrust(
            this IReadOnlyCollection<X509Certificate> certChain)
        {
            new { certChain }.Must().NotBeNull().And().NotBeEmptyEnumerable<X509Certificate>().OrThrowFirstFailure();

            var result = certChain.OrderCertChainFromHighestToLowestLevelOfTrust().Reverse().ToList();
            return result;
        }

        /// <summary>
        /// Re-orders a certificate chain from highest to lowest level of trust.
        /// </summary>
        /// <param name="certChain">The certificate chain to re-order.</param>
        /// <returns>
        /// The certificates in the specified chain, ordered from highest to lowest level of trust.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="certChain"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="certChain"/> is empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="certChain"/> is malformed.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is a good use of catching general exception types.")]
        public static IReadOnlyList<X509Certificate> OrderCertChainFromHighestToLowestLevelOfTrust(
            this IReadOnlyCollection<X509Certificate> certChain)
        {
            new { certChain }.Must().NotBeNull().And().NotBeEmptyEnumerable<X509Certificate>().OrThrowFirstFailure();

            certChain = certChain.Distinct().ToList();

            // for every cert, record which other certs verify it
            var parentCertsByChildCert = new Dictionary<X509Certificate, List<X509Certificate>>();
            foreach (var cert in certChain)
            {
                parentCertsByChildCert.Add(cert, new List<X509Certificate>());

                var otherCerts = certChain.Except(new[] { cert }).ToList();
                foreach (var otherCert in otherCerts)
                {
                    // ReSharper disable EmptyGeneralCatchClause
                    try
                    {
                        cert.Verify(otherCert.GetPublicKey());
                        parentCertsByChildCert[cert].Add(otherCert);
                    }
                    catch (Exception)
                    {
                    }

                    // ReSharper restore EmptyGeneralCatchClause
                }
            }

            // any cert has two parents?
            if (parentCertsByChildCert.Values.Any(_ => _.Count > 1))
            {
                throw new ArgumentException("the cert chain is malformed");
            }

            // should only be one cert with no parent
            if (parentCertsByChildCert.Values.Count(_ => !_.Any()) != 1)
            {
                throw new ArgumentException("the cert chain is malformed");
            }

            // identify and remove the root cert, the remaining certs should have only one parent
            var rootCert = parentCertsByChildCert.Single(_ => !_.Value.Any()).Key;
            parentCertsByChildCert.Remove(rootCert);

            // no two certs should have the same parent
            if (parentCertsByChildCert.SelectMany(_ => _.Value).Distinct().Count() != parentCertsByChildCert.Count)
            {
                throw new ArgumentException("the cert chain is malformed");
            }

            // flip it and index the certs by parent
            var childCertByParentCert = parentCertsByChildCert.ToDictionary(_ => _.Value.Single(), _ => _.Key);
            var result = new List<X509Certificate> { rootCert };
            while (childCertByParentCert.ContainsKey(result.Last()))
            {
                result.Add(childCertByParentCert[result.Last()]);
            }

            return result;
        }

        /// <summary>
        /// Reads one or more certs encoded in PEM.
        /// </summary>
        /// <param name="pemEncodedCerts">The PEM encoded certificates.</param>
        /// <returns>
        /// The certificates.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="pemEncodedCerts"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="pemEncodedCerts"/> is white space.</exception>
        public static IReadOnlyList<X509Certificate> ReadCertsFromPemEncodedString(
            string pemEncodedCerts)
        {
            new { pemEncodedCerts }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            // remove empty lines - required so that PemReader.ReadObject doesn't return null in-between returning certs
            pemEncodedCerts = Regex.Replace(pemEncodedCerts, @"^\s*$[\r\n]*", string.Empty, RegexOptions.Multiline);

            var result = new List<X509Certificate>();
            using (var stringReader = new StringReader(pemEncodedCerts))
            {
                var pemReader = new PemReader(stringReader);
                var certObject = pemReader.ReadObject();
                while (certObject != null)
                {
                    var cert = certObject as X509Certificate;
                    result.Add(cert);
                    certObject = pemReader.ReadObject();
                }
            }

            return result;
        }

        /// <summary>
        /// Extracts a certificate chain from PKCS#7 CMS payload encoded in PEM.
        /// </summary>
        /// <param name="pemEncodedPkcs7">The payload containing the PKCS#7 CMS data.</param>
        /// <remarks>
        /// The method is expecting a PKCS#7/CMS SignedData structure containing no "content" and zero SignerInfos.
        /// </remarks>
        /// <returns>
        /// The certificate chain contained in the specified payload.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="pemEncodedPkcs7"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="pemEncodedPkcs7"/> is white space.</exception>
        public static IReadOnlyList<X509Certificate> ReadCertChainFromPemEncodedPkcs7CmsString(
            string pemEncodedPkcs7)
        {
            new { pemEncodedPkcs7 }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            IReadOnlyList<X509Certificate> result;
            using (var stringReader = new StringReader(pemEncodedPkcs7))
            {
                var pemReader = new PemReader(stringReader);
                var pemObject = pemReader.ReadPemObject();
                var data = new CmsSignedData(pemObject.Content);
                var certStore = data.GetCertificates("COLLECTION");
                result = certStore.GetMatches(null).Cast<X509Certificate>().ToList();
            }

            return result;
        }

        /// <summary>
        /// Reads a certificate signing request encoded in PEM.
        /// </summary>
        /// <param name="pemEncodedCsr">The PEM encoded certificate signing request.</param>
        /// <returns>
        /// The certificate signing request.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="pemEncodedCsr"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="pemEncodedCsr"/> is white space.</exception>
        public static Pkcs10CertificationRequest ReadCsrFromPemEncodedString(
            string pemEncodedCsr)
        {
            new { pemEncodedCsr }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            Pkcs10CertificationRequest result;
            using (var stringReader = new StringReader(pemEncodedCsr))
            {
                var pemReader = new PemReader(stringReader);
                var pemObject = pemReader.ReadPemObject();
                result = new Pkcs10CertificationRequest(pemObject.Content);
            }

            return result;
        }

        /// <summary>
        /// Reads a private key encoded in PEM.
        /// </summary>
        /// <param name="pemEncodedPrivateKey">The PEM encoded private key.</param>
        /// <returns>
        /// The private key.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="pemEncodedPrivateKey"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="pemEncodedPrivateKey"/> is white space.</exception>
        public static AsymmetricKeyParameter ReadPrivateKeyFromPemEncodedString(
            string pemEncodedPrivateKey)
        {
            new { pemEncodedPrivateKey }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            AsymmetricCipherKeyPair keyPair;
            using (var stringReader = new StringReader(pemEncodedPrivateKey))
            {
                var pemReader = new PemReader(stringReader);
                keyPair = pemReader.ReadObject() as AsymmetricCipherKeyPair;
            }

            AsymmetricKeyParameter result = null;
            if (keyPair != null)
            {
                result = keyPair.Private;
            }

            return result;
        }

        /// <summary>
        /// Gets the X509 field values from a certificate.
        /// </summary>
        /// <param name="cert">The certificate.</param>
        /// <returns>
        /// The X509 field values indexed by the kind of field.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="cert"/> is null.</exception>
        public static IReadOnlyDictionary<X509FieldKind, string> GetX509Fields(
            this X509Certificate cert)
        {
            new { cert }.Must().NotBeNull().OrThrow();

            var result = new Dictionary<X509FieldKind, string>
            {
                { X509FieldKind.IssuerName, cert.IssuerDN?.ToString() },
                { X509FieldKind.NotAfter, cert.NotAfter.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) },
                { X509FieldKind.NotBefore, cert.NotBefore.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) },
                { X509FieldKind.SerialNumber, cert.SerialNumber?.ToString() },
                { X509FieldKind.SignatureAlgorithmName, cert.SigAlgName },
                { X509FieldKind.SubjectName, cert.SubjectDN?.ToString() },
                { X509FieldKind.Version, cert.Version.ToString(CultureInfo.InvariantCulture) }
            };
            return result;
        }

        /// <summary>
        /// Gets the range of time over which a certificate is valid.
        /// </summary>
        /// <param name="cert">The certificate.</param>
        /// <returns>
        /// The range of time over which the specified certificate is valid.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="cert"/> is null.</exception>
        public static DateTimeRange GetValidityPeriod(
            this X509Certificate cert)
        {
            new { cert }.Must().NotBeNull().OrThrow();

            var result = new DateTimeRange(cert.NotBefore, cert.NotAfter);

            return result;
        }

        /// <summary>
        /// Gets the X509 subject attribute values from a certificate signing request.
        /// </summary>
        /// <param name="csr">The certificate signing request.</param>
        /// <returns>
        /// The X509 subject attribute values indexed by the kind of subject attribute.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="csr"/> is null.</exception>
        public static IReadOnlyDictionary<X509SubjectAttributeKind, string> GetX509SubjectAttributes(
            this Pkcs10CertificationRequest csr)
        {
            new { csr }.Must().NotBeNull().OrThrow();

            var subject = csr.GetCertificationRequestInfo().Subject;

            var result = subject.GetX509SubjectAttributes();

            return result;
        }

        /// <summary>
        /// Gets the X509 subject attribute values from a certificate.
        /// </summary>
        /// <param name="cert">The certificate.</param>
        /// <returns>
        /// The X509 subject attribute values indexed by the kind of subject attribute.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="cert"/> is null.</exception>
        public static IReadOnlyDictionary<X509SubjectAttributeKind, string> GetX509SubjectAttributes(
            this X509Certificate cert)
        {
            new { cert }.Must().NotBeNull().OrThrow();

            var subject = cert.SubjectDN;

            var result = subject.GetX509SubjectAttributes();

            return result;
        }

        /// <summary>
        /// Gets the X509 subject attribute values from a subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <returns>
        /// The X509 subject attribute values indexed by the kind of subject attribute.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="subject"/> is null.</exception>
        public static IReadOnlyDictionary<X509SubjectAttributeKind, string> GetX509SubjectAttributes(
            this X509Name subject)
        {
            new { subject }.Must().NotBeNull().OrThrow();

            var objectIds = subject.GetOidList();
            var values = subject.GetValueList();

            var result = new Dictionary<X509SubjectAttributeKind, string>();

            for (int x = 0; x < objectIds.Count; x++)
            {
                var derId = objectIds[x] as DerObjectIdentifier;
                var value = values[x] as string;

                if ((derId != null) && (value != null))
                {
                    if (derId.Id == X509Name.C.Id)
                    {
                        result.Add(X509SubjectAttributeKind.Country, value);
                    }
                    else if (derId.Id == X509Name.O.Id)
                    {
                        result.Add(X509SubjectAttributeKind.Organization, value);
                    }
                    else if (derId.Id == X509Name.OU.Id)
                    {
                        result.Add(X509SubjectAttributeKind.OrganizationalUnit, value);
                    }
                    else if (derId.Id == X509Name.T.Id)
                    {
                        result.Add(X509SubjectAttributeKind.Title, value);
                    }
                    else if (derId.Id == X509Name.CN.Id)
                    {
                        result.Add(X509SubjectAttributeKind.CommonName, value);
                    }
                    else if (derId.Id == X509Name.Street.Id)
                    {
                        result.Add(X509SubjectAttributeKind.Street, value);
                    }
                    else if (derId.Id == X509Name.SerialNumber.Id)
                    {
                        result.Add(X509SubjectAttributeKind.SerialNumber, value);
                    }
                    else if (derId.Id == X509Name.L.Id)
                    {
                        result.Add(X509SubjectAttributeKind.Locality, value);
                    }
                    else if (derId.Id == X509Name.ST.Id)
                    {
                        result.Add(X509SubjectAttributeKind.State, value);
                    }
                }
            }

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
        private static Pkcs10CertificationRequest CreateCsr(
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
