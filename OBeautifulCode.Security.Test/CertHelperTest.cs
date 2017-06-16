// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CertHelperTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Test
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using OBeautifulCode.Reflection;

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
        public static void ReadCsrFromPemEncodedString___Should_roundtrip_to_pem_encoded_csr___When_called()
        {
            // Arrange
            var expected = AssemblyHelper.ReadEmbeddedResourceAsString("csr.pem");

            // Act
            var csr = CertHelper.ReadCsrFromPemEncodedString(expected);

            // Assert
            expected.RemoveLineBreaks().Should().Be(csr.AsPemEncodedString().RemoveLineBreaks());
        }

        [Fact]
        public static void GetX509SubjectAttributes___Should_throw_ArgumentNullException___When_parameter_csr_is_null()
        {
            // Arrange, Act
            var ex = Record.Exception(() => CertHelper.GetX509SubjectAttributes(null));

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex.Should().BeOfType<ArgumentNullException>();
            ex.Message.Should().Contain("csr");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void GetX509SubjectAttributes___Should_return_subject_attribute_values_of_csr___When_called()
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

        // ReSharper restore InconsistentNaming
    }
}
