// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AwsCertificateManagerPayloadTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Recipes.Test
{
    using System;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public static class AwsCertificateManagerPayloadTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_certificateBody_is_null()
        {
            // Arrange
            var certificatePrivateKey = A.Dummy<string>();
            var certificateChain = A.Dummy<string>();

            // Act
            var ex = Record.Exception(() => new AwsCertificateManagerPayload(null, certificatePrivateKey, certificateChain));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("certificateBody");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_certificateBody_is_white_space()
        {
            // Arrange
            var certificatePrivateKey = A.Dummy<string>();
            var certificateChain = A.Dummy<string>();

            // Act
            var ex = Record.Exception(() => new AwsCertificateManagerPayload(" \r\n  ", certificatePrivateKey, certificateChain));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Contain("certificateBody");
            ex.Message.Should().Contain("white space");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_certificatePrivateKey_is_null()
        {
            // Arrange
            var certificateBody = A.Dummy<string>();
            var certificateChain = A.Dummy<string>();

            // Act
            var ex = Record.Exception(() => new AwsCertificateManagerPayload(certificateBody, null, certificateChain));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("certificatePrivateKey");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_certificatePrivateKey_is_white_space()
        {
            // Arrange
            var certificateBody = A.Dummy<string>();
            var certificateChain = A.Dummy<string>();

            // Act
            var ex = Record.Exception(() => new AwsCertificateManagerPayload(certificateBody, " \r\n ", certificateChain));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Contain("certificatePrivateKey");
            ex.Message.Should().Contain("white space");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentNullException___When_parameter_certificateChain_is_null()
        {
            // Arrange
            var certificateBody = A.Dummy<string>();
            var certificatePrivateKey = A.Dummy<string>();

            // Act
            var ex = Record.Exception(() => new AwsCertificateManagerPayload(certificateBody, certificatePrivateKey, null));

            // Assert
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("certificateChain");
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_certificateChain_is_white_space()
        {
            // Arrange
            var certificateBody = A.Dummy<string>();
            var certificatePrivateKey = A.Dummy<string>();

            // Act
            var ex = Record.Exception(() => new AwsCertificateManagerPayload(certificateBody, certificatePrivateKey, "  \r\n  "));

            // Assert
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Contain("certificateChain");
            ex.Message.Should().Contain("white space");
        }

        [Fact]
        public static void CertificateBody___Should_return_same_certificateBody_passed_to_constructor___When_getting()
        {
            // Arrange
            var certificateBody = A.Dummy<string>();
            var certificatePrivateKey = A.Dummy<string>();
            var certificateChain = A.Dummy<string>();
            var systemUnderTest = new AwsCertificateManagerPayload(certificateBody, certificatePrivateKey, certificateChain);

            // Act
            var actual = systemUnderTest.CertificateBody;

            // Assert
            actual.Should().Be(certificateBody);
        }

        [Fact]
        public static void CertificatePrivateKey___Should_return_same_certificatePrivateKey_passed_to_constructor___When_getting()
        {
            // Arrange
            var certificateBody = A.Dummy<string>();
            var certificatePrivateKey = A.Dummy<string>();
            var certificateChain = A.Dummy<string>();
            var systemUnderTest = new AwsCertificateManagerPayload(certificateBody, certificatePrivateKey, certificateChain);

            // Act
            var actual = systemUnderTest.CertificatePrivateKey;

            // Assert
            actual.Should().Be(certificatePrivateKey);
        }

        [Fact]
        public static void CertificateChain___Should_return_same_certificateChain_passed_to_constructor___When_getting()
        {
            // Arrange
            var certificateBody = A.Dummy<string>();
            var certificatePrivateKey = A.Dummy<string>();
            var certificateChain = A.Dummy<string>();
            var systemUnderTest = new AwsCertificateManagerPayload(certificateBody, certificatePrivateKey, certificateChain);

            // Act
            var actual = systemUnderTest.CertificateChain;

            // Assert
            actual.Should().Be(certificateChain);
        }
    }
}
