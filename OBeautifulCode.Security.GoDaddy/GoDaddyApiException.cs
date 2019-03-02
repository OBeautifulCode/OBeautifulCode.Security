// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GoDaddyApiException.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    using System;

    using static System.FormattableString;

    /// <summary>
    /// The exception that is thrown when a call to the GoDaddy API results an a non-successful response.
    /// </summary>
    public class GoDaddyApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GoDaddyApiException"/> class.
        /// </summary>
        public GoDaddyApiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoDaddyApiException"/> class
        /// with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GoDaddyApiException(
            string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoDaddyApiException"/> class with the specified error message,
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public GoDaddyApiException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoDaddyApiException"/> class with details of the
        /// error that occured.
        /// </summary>
        /// <param name="certificateApiMethod">The API method that was called.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <param name="certificateApiErrorKind">The kind (interpretation) of the error that occured.</param>
        /// <param name="contentJson">The JSON-formatted content of the response.</param>
        public GoDaddyApiException(
            CertificateApiMethod certificateApiMethod,
            int statusCode,
            CertificateApiErrorKind certificateApiErrorKind,
            string contentJson)
            : base(Invariant($"Call to {certificateApiMethod} resulted in a {statusCode} error, which can be interpreted as {certificateApiErrorKind}.  The JSON-formatted response content: {Environment.NewLine}{contentJson}"))
        {
            this.CertificateApiMethod = certificateApiMethod;
            this.StatusCode = statusCode;
            this.CertificateApiErrorKind = certificateApiErrorKind;
            this.ContentJson = contentJson;
        }

        /// <summary>
        /// Gets the API method that was called.
        /// </summary>
        public CertificateApiMethod CertificateApiMethod { get; }

        /// <summary>
        /// Gets the HTTP response status code.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Gets the kind (interpretation) of the error that occured.
        /// </summary>
        public CertificateApiErrorKind CertificateApiErrorKind { get; }

        /// <summary>
        /// Gets the JSON-formatted content of the response.
        /// </summary>
        public string ContentJson { get; }
    }
}