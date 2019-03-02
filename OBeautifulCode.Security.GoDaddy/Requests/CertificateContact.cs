// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CertificateContact.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    /// <summary>
    /// Contact details of a certificate requestor.
    /// </summary>
    public class CertificateContact
    {
        /// <summary>
        /// Gets or sets the email address of the requestor.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the job title of the requestor.  Only used for extended validation SSL.
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Gets or sets the first name of the requestor.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string NameFirst { get; set; }

        /// <summary>
        /// Gets or sets the last name of the requestor.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string NameLast { get; set; }

        /// <summary>
        /// Gets or sets the middle initial of the requestor.
        /// </summary>
        public string NameMiddle { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the requestor.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the suffix of the requestor.
        /// </summary>
        public string Suffix { get; set; }
    }
}