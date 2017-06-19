// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CertHelperTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using FluentAssertions;

    using OBeautifulCode.Reflection;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.X509;

    using Xunit;

    public static class CertHelperTest
    {
        [Fact]
        public static void ReadCsrFromPemEncodedString___Should_throw_ArgumentNullException___When_parameter_pemEncodedCsr_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => CertHelper.ReadCsrFromPemEncodedString(null));

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("pemEncodedCsr");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void ReadCsrFromPemEncodedString___Should_throw_ArgumentException___When_parameter_pemEncodedCsr_is_white_space()
        {
            // Arrange, Act
            var ex = Record.Exception(() => CertHelper.ReadCsrFromPemEncodedString("  \r\n "));

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Contain("pemEncodedCsr");
            ex.Message.Should().Contain("white space");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void ReadCsrFromPemEncodedString___Should_roundtrip_to_PEM_encoded_CSR___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("csr.pem");

            // Act
            var csr = CertHelper.ReadCsrFromPemEncodedString(expected);

            // Assert
            expected.RemoveLineBreaks().Should().Be(csr.AsPemEncodedString().RemoveLineBreaks());
        }

        [Fact]
        public static void ReadCertsFromPemEncodedString___Should_throw_ArgumentNullException___When_parameter_pemEncodedCerts_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => CertHelper.ReadCertsFromPemEncodedString(null));

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("pemEncodedCerts");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void ReadCertsFromPemEncodedString___Should_roundtrip_to_same_PEM_encoded_string___When_writing_the_cert_chain_to_PEM()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("cert-chain.pem");

            // Act
            var actual = CertHelper.ReadCertsFromPemEncodedString(expected);

            // Assert
            actual.AsPemEncodedString().RemoveLineBreaks().Should().Be(expected.RemoveLineBreaks());
        }

        [Fact]
        public static void ReadCertChainFromPemEncodedPkcs7CmsString___Should_throw_ArgumentNullException___When_parameter_pemEncodedPkcs7_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => CertHelper.ReadCertChainFromPemEncodedPkcs7CmsString(null));

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("pemEncodedPkcs7");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void ReadCertChainFromPemEncodedPkcs7CmsString___Should_create_a_cert_chain_that_produces_an_expected_PEM_encoded_string___When_writing_the_cert_chain_to_PEM()
        {
            // Arrange
            var pemEncodedPkcs7 = AssemblyHelper.ReadEmbeddedResourceAsString("pkcs7-cert-chain.pem");
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("cert-chain.pem");

            // Act
            var actual = CertHelper.ReadCertChainFromPemEncodedPkcs7CmsString(pemEncodedPkcs7);

            // Assert
            actual.AsPemEncodedString().RemoveLineBreaks().Should().Be(expected.RemoveLineBreaks());
        }

        [Fact]
        public static void ReadPrivateKeyFromPemEncodedString___Should_throw_ArgumentNullException___When_parameter_pemEncodedPrivateKey_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => CertHelper.ReadPrivateKeyFromPemEncodedString(null));

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("pemEncodedPrivateKe");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void ReadPrivateKeyFromPemEncodedString___Should_roundtrip_to_same_PEM_encoded_string___When_writing_the_private_key_to_PEM()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("private-key.pem");

            // Act
            var actual = CertHelper.ReadPrivateKeyFromPemEncodedString(expected);

            // Assert
            actual.AsPemEncodedString().RemoveLineBreaks().Should().Be(expected.RemoveLineBreaks());
        }

        [Fact]
        public static void GetX509SubjectAttributes_Pkcs10CertificationRequest___Should_throw_ArgumentNullException___When_parameter_csr_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((Pkcs10CertificationRequest)null).GetX509SubjectAttributes());

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("csr");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void GetX509SubjectAttributes__Pkcs10CertificationRequest___Should_return_subject_attribute_values_of_csr___When_called()
        {
            // Arrange
            var expected = new Dictionary<X509SubjectAttributeKind, string>
            {
                { X509SubjectAttributeKind.CommonName, "example.digicert.com" },
                { X509SubjectAttributeKind.Organization, "DigiCert Inc." },
                { X509SubjectAttributeKind.OrganizationalUnit, "DigiCert" },
                { X509SubjectAttributeKind.Locality, "Lindon" },
                { X509SubjectAttributeKind.State, "Utah" },
                { X509SubjectAttributeKind.Country, "US" }
            };

            var pemEncodedCsr = AssemblyHelper.ReadEmbeddedResourceAsString("csr.pem");
            var csr = CertHelper.ReadCsrFromPemEncodedString(pemEncodedCsr);

            // Act
            var actual = csr.GetX509SubjectAttributes();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public static void GetX509SubjectAttributes_X509Certificate___Should_throw_ArgumentNullException___When_parameter_cert_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((X509Certificate)null).GetX509SubjectAttributes());

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("csr");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void GetX509SubjectAttributes__X509Certificate___Should_return_subject_attribute_values_of_certificate___When_called()
        {
            // Arrange
            var expected = new Dictionary<X509SubjectAttributeKind, string>
            {
                { X509SubjectAttributeKind.CommonName, "Starfield Secure Certificate Authority - G2" },
                { X509SubjectAttributeKind.Organization, "Starfield Technologies, Inc." },
                { X509SubjectAttributeKind.OrganizationalUnit, "http://certs.starfieldtech.com/repository/" },
                { X509SubjectAttributeKind.Locality, "Scottsdale" },
                { X509SubjectAttributeKind.State, "Arizona" },
                { X509SubjectAttributeKind.Country, "US" }
            };

            var certChain = AssemblyHelper.ReadEmbeddedResourceAsString("cert-chain.pem");
            var cert = CertHelper.ReadCertsFromPemEncodedString(certChain).First();

            // Act
            var actual = cert.GetX509SubjectAttributes();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public static void GetX509SubjectAttributes_X509Name__Should_throw_ArgumentNullException___When_parameter_subject_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((X509Name)null).GetX509SubjectAttributes());

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("csr");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void GetX509SubjectAttributes__X509Name___Should_return_subject_attribute_values_of_subject___When_called()
        {
            // Arrange
            var expected = new Dictionary<X509SubjectAttributeKind, string>
            {
                { X509SubjectAttributeKind.CommonName, "example.digicert.com" },
                { X509SubjectAttributeKind.Organization, "DigiCert Inc." },
                { X509SubjectAttributeKind.OrganizationalUnit, "DigiCert" },
                { X509SubjectAttributeKind.Locality, "Lindon" },
                { X509SubjectAttributeKind.State, "Utah" },
                { X509SubjectAttributeKind.Country, "US" }
            };

            var pemEncodedCsr = AssemblyHelper.ReadEmbeddedResourceAsString("csr.pem");
            var csr = CertHelper.ReadCsrFromPemEncodedString(pemEncodedCsr);
            var subject = csr.GetCertificationRequestInfo().Subject;

            // Act
            var actual = subject.GetX509SubjectAttributes();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        // ReSharper restore InconsistentNaming
    }
}
