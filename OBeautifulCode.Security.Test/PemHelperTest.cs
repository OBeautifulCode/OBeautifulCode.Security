// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PemHelperTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using FluentAssertions;

    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Security.Recipes;

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.OpenSsl;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.X509;

    using Xunit;

    public static class PemHelperTest
    {
        [Fact]
        public static void AsPemEncodedString_Pkcs10CertificationRequest___Should_throw_ArgumentNullException___When_parameter_csr_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((Pkcs10CertificationRequest)null).AsPemEncodedString());

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("csr");
        }

        [Fact]
        public static void AsPemEncodedString_Pkcs10CertificationRequest___Should_return_csr_encoded_in_PEM___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("csr.pem");
            var csr = CertHelper.ReadCsrFromPemEncodedString(expected);

            // Act
            var actual = csr.AsPemEncodedString().RemoveLineBreaks();

            // Assert
            actual.Should().Be(expected.RemoveLineBreaks());
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricKeyParameter___Should_throw_ArgumentNullException___When_parameter_key_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((AsymmetricKeyParameter)null).AsPemEncodedString());

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("key");
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricKeyParameter___Should_return_private_key_encoded_in_PEM___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("private-key.pem");
            var privateKey = CertHelper.ReadPrivateKeyFromPemEncodedString(expected);

            // Act
            var actual = privateKey.AsPemEncodedString().RemoveLineBreaks();

            // Assert
            actual.Should().Be(expected.RemoveLineBreaks());
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricKeyParameter___Should_return_public_key_encoded_in_PEM___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("public-key.pem");
            var publicKey = ReadPublicKeyFromPemEncodedString(expected);

            // Act
            var actual = publicKey.AsPemEncodedString().RemoveLineBreaks();

            // Assert
            actual.Should().Be(expected.RemoveLineBreaks());
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricCipherKeyPair___Should_throw_ArgumentNullException___When_parameter_keyPair_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((AsymmetricCipherKeyPair)null).AsPemEncodedString());

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("keyPair");
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricCipherKeyPair___Should_return_private_key_encoded_in_PEM___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("private-key.pem");
            var privateKeyPair = ReadAsymmetricKeyPairFromPemEncodedString(expected);

            // Act
            var actual = privateKeyPair.AsPemEncodedString().RemoveLineBreaks();

            // Assert
            actual.Should().Be(expected.RemoveLineBreaks());
        }

        [Fact]
        public static void AsPemEncodedString_IReadOnlyList_of_X509Certificate___Should_throw_ArgumentNullException___When_parameter_certChain_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((IReadOnlyList<X509Certificate>)null).AsPemEncodedString());

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("certChain");
        }

        [Fact]
        public static void AsPemEncodedString_IReadOnlyList_of_X509Certificate___Should_return_certificates_encoded_in_PEM___When_called()
        {
            // Arrange
            var pkcs7CertChainInPem = AssemblyHelper.ReadEmbeddedResourceAsString("pkcs7-cert-chain.pem");
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("cert-chain.pem");
            var certChain = CertHelper.ReadCertChainFromPemEncodedPkcs7CmsString(pkcs7CertChainInPem);

            // Act
            var actual = certChain.AsPemEncodedString().RemoveLineBreaks();

            // Assert
            actual.Should().Be(expected.RemoveLineBreaks());
        }

        private static AsymmetricKeyParameter ReadPublicKeyFromPemEncodedString(
            string pemEncodedKey)
        {
            AsymmetricKeyParameter result;
            using (var stringReader = new StringReader(pemEncodedKey))
            {
                var pemReader = new PemReader(stringReader);
                var pemObject = pemReader.ReadObject();
                result = (RsaKeyParameters)pemObject;
            }

            return result;
        }

        private static AsymmetricCipherKeyPair ReadAsymmetricKeyPairFromPemEncodedString(
            string pemEncodedKeyPair)
        {
            AsymmetricCipherKeyPair result;
            using (var stringReader = new StringReader(pemEncodedKeyPair))
            {
                var pemReader = new PemReader(stringReader);
                var pemObject = pemReader.ReadObject();
                result = (AsymmetricCipherKeyPair)pemObject;
            }

            return result;
        }
    }
}
