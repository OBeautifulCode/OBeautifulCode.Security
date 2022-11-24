// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtractedPfxFileTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Recipes.Test
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using FluentAssertions;
    using OBeautifulCode.Reflection.Recipes;
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
            var ex = Record.Exception(() => new ExtractedPfxFile(new X509Certificate2[] { }, privateKey));

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
            ex.Message.Should().Contain("contains an element that is null");
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
