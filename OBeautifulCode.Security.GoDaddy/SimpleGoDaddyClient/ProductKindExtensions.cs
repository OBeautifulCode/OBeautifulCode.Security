// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductKindExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    using System;
    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Extension methods on <see cref="ProductKind"/>.
    /// </summary>
    public static class ProductKindExtensions
    {
        /// <summary>
        /// Converts a certificate product kind to a string representation
        /// that can be used with the GoDaddy API.
        /// </summary>
        /// <param name="productKind">The kind of certificate product.</param>
        /// <returns>
        /// A string representation of the specified certificate product kind
        /// that can be used with the GoDaddy API.
        /// </returns>
        public static string ToApiCompatibleString(
            this ProductKind productKind)
        {
            new { productKind }.Must().NotBeEqualTo(ProductKind.Unknown);

            switch (productKind)
            {
                case ProductKind.DomainValidationSsl:
                    return "DV_SSL";
                case ProductKind.DomainValidationWildcardSsl:
                    return "DV_WILDCARD_SSL";
                case ProductKind.ExtendedValidationSsl:
                    return "EV_SSL";
                case ProductKind.OrganizationValidationCodeSigning:
                    return "OV_CS";
                case ProductKind.OrganizationValidationDriverSigning:
                    return "OV_DS";
                case ProductKind.OrganizationValidationSsl:
                    return "OV_SSL";
                case ProductKind.OrganizationValidationWildcardSsl:
                    return "OV_WILDCARD_SSL";
                case ProductKind.UnifiedCommunicationDomainValidationSsl:
                    return "UCC_DV_SSL";
                case ProductKind.UnifiedCommunicationExtendedValidationSsl:
                    return "UCC_EV_SSL";
                case ProductKind.UnifiedCommunicationOrganizationValidationSsl:
                    return "UCC_OV_SSL";
            }

            throw new NotSupportedException("This certificate product kind is not supported: " + productKind);
        }
    }
}
