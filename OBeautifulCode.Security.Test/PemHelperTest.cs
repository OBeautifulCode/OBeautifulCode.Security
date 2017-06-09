// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PemHelperTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Test
{
    using System;
    using System.IO;

    using FluentAssertions;

    using OBeautifulCode.Reflection;

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.OpenSsl;
    using Org.BouncyCastle.Pkcs;

    using Xunit;

    public static class PemHelperTest
    {
        [Fact]
        public static void AsPemEncodedString_Pkcs10CertificationRequest___Should_throw_ArgumentNullException___When_parameter_csr_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((Pkcs10CertificationRequest)null).AsPemEncodedString());

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("csr");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void AsPemEncodedString_Pkcs10CertificationRequest___Should_return_csr_encoded_in_PEM___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("csr.pem");
            var csr = ReadCsrFromPemEncodedString(expected);

            // Act
            var actual = csr.AsPemEncodedString();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricKeyParameter___Should_throw_ArgumentNullException___When_parameter_key_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((AsymmetricKeyParameter)null).AsPemEncodedString());

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("key");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricKeyParameter___Should_return_private_key_encoded_in_PEM___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("private-key.pem");
            var privateKey = ReadPrivateKeyFromPemEncodedString(expected);

            // Act
            var actual = privateKey.AsPemEncodedString();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricKeyParameter___Should_return_public_key_encoded_in_PEM___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("public-key.pem");
            var publicKey = ReadPublicKeyFromPemEncodedString(expected);

            // Act
            var actual = publicKey.AsPemEncodedString();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricCipherKeyPair___Should_throw_ArgumentNullException___When_parameter_keyPair_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => ((AsymmetricCipherKeyPair)null).AsPemEncodedString());

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("keyPair");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void AsPemEncodedString_AsymmetricCipherKeyPair___Should_return_private_key_encoded_in_PEM___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("private-key.pem");
            var privateKeyPair = ReadAsymmetricKeyPairFromPemEncodedString(expected);

            // Act
            var actual = privateKeyPair.AsPemEncodedString();

            // Assert
            actual.Should().Be(expected);
        }

        private static Pkcs10CertificationRequest ReadCsrFromPemEncodedString(
            string pemEncodedCsr)
        {
            Pkcs10CertificationRequest result;
            using (var stringReader = new StringReader(pemEncodedCsr))
            {
                var pemReader = new PemReader(stringReader);
                var pemObject = pemReader.ReadPemObject();
                result = new Pkcs10CertificationRequest(pemObject.Content);
            }

            return result;
        }

        private static AsymmetricKeyParameter ReadPrivateKeyFromPemEncodedString(
            string pemEncodedKey)
        {
            var result = ReadAsymmetricKeyPairFromPemEncodedString(pemEncodedKey).Private;
            return result;
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

        // ReSharper restore InconsistentNaming
    }
}
