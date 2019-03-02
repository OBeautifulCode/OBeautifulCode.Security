// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GoDaddyEnvironment.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    /// <summary>
    /// Specifies the GoDaddy environment within which to manipulate resources.
    /// </summary>
    public enum GoDaddyEnvironment
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// The operating test environment (OTE).
        /// </summary>
        OperatingTestEnvironment,

        /// <summary>
        /// The production environment.
        /// </summary>
        Production,
    }
}
