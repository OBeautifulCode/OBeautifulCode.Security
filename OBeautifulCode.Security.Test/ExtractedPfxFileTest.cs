// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtractedPfxFileTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Test
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Security.Recipes;

    using Org.BouncyCastle.X509;

    using Xunit;

    public static class ExtractedPfxFileTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_certificateChain_is_null()
        {
            // Arrange
            var privateKey = CertHelper.CreateRsaKeyPair().Private;

            // Act
            var ex = Record.Exception(() => new ExtractedPfxFile(null, privateKey));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("certificateChain");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_certificateChain_is_empty()
        {
            // Arrange
            var privateKey = CertHelper.CreateRsaKeyPair().Private;

            // Act
            var ex = Record.Exception(() => new ExtractedPfxFile(new X509Certificate[] { }, privateKey));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Contain("certificateChain");
            ex.Message.Should().Contain("empty");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_certificateChain_contains_null_element()
        {
            // Arrange
            var privateKey = CertHelper.CreateRsaKeyPair().Private;
            var pemEncodedCertChain = AssemblyHelper.ReadEmbeddedResourceAsString("cert-chain.pem");
            var certChain = CertHelper.ReadCertsFromPemEncodedString(pemEncodedCertChain).ToList();
            certChain.Add(null);

            // Act
            var ex = Record.Exception(() => new ExtractedPfxFile(certChain, privateKey));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Contain("certificateChain");
            ex.Message.Should().Contain("contains at least one null element");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_privateKey_is_null()
        {
            // Arrange
            var pemEncodedCertChain = AssemblyHelper.ReadEmbeddedResourceAsString("cert-chain.pem");
            var certChain = CertHelper.ReadCertsFromPemEncodedString(pemEncodedCertChain).ToList();

            // Act
            var ex = Record.Exception(() => new ExtractedPfxFile(certChain, null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("privateKey");
        }

        [Fact]
        public static void CertificateChain___Should_return_same_certificateChain_passed_to_constructor___When_getting()
        {
            // Arrange
            var privateKey = CertHelper.CreateRsaKeyPair().Private;
            var pemEncodedCertChain = AssemblyHelper.ReadEmbeddedResourceAsString("cert-chain.pem");
            var certChain = CertHelper.ReadCertsFromPemEncodedString(pemEncodedCertChain).ToList();
            var systemUnderTest = new ExtractedPfxFile(certChain, privateKey);

            // Act
            var actual = systemUnderTest.CertificateChain;

            // Assert
            actual.Should().BeSameAs(certChain);
        }

        [Fact]
        public static void PrivateKey___Should_return_same_privateKey_passed_to_constructor___When_getting()
        {
            // Arrange
            var privateKey = CertHelper.CreateRsaKeyPair().Private;
            var pemEncodedCertChain = AssemblyHelper.ReadEmbeddedResourceAsString("cert-chain.pem");
            var certChain = CertHelper.ReadCertsFromPemEncodedString(pemEncodedCertChain).ToList();
            var systemUnderTest = new ExtractedPfxFile(certChain, privateKey);

            // Act
            var actual = systemUnderTest.PrivateKey;

            // Assert
            actual.Should().BeSameAs(privateKey);
        }
    }
}
