// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    /// <summary>
    /// Specifies the kind of certificate product.
    /// </summary>
    public enum ProductKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// A domain validated SSL certificate.
        /// </summary>
        DomainValidationSsl,

        /// <summary>
        /// The domain validated wildcard SSL certificate.
        /// </summary>
        DomainValidationWildcardSsl,

        /// <summary>
        /// An extended validation SSL certificate.
        /// </summary>
        ExtendedValidationSsl,

        /// <summary>
        /// An organization validation code signing certificate.
        /// </summary>
        OrganizationValidationCodeSigning,

        /// <summary>
        /// An organization validation driver signing certificate.
        /// </summary>
        OrganizationValidationDriverSigning,

        /// <summary>
        /// An organization validation SSL certificate.
        /// </summary>
        OrganizationValidationSsl,

        /// <summary>
        /// An organization validation wildcard SSL certificate.
        /// </summary>
        OrganizationValidationWildcardSsl,

        /// <summary>
        /// A UCC domain validation SSL certificate.
        /// </summary>
        UnifiedCommunicationDomainValidationSsl,

        /// <summary>
        /// A UCC extended validation SSL certificate.
        /// </summary>
        UnifiedCommunicationExtendedValidationSsl,

        /// <summary>
        /// A UCC organization validation SSL certificate.
        /// </summary>
        UnifiedCommunicationOrganizationValidationSsl,
    }
}
