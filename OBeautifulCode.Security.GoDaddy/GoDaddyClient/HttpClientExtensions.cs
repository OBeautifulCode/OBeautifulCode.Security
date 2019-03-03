// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpClientExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Naos.Serialization.Domain;
    using Naos.Serialization.Json;

    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Extension methods on type <see cref="HttpClient"/>.
    /// </summary>
    internal static class HttpClientExtensions
    {
        private const string JsonMediaType = "application/json";

        private static readonly ISerialize Serializer = new NaosJsonSerializer(SerializationKind.Minimal);

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="client">The http client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="content">The content to send.</param>
        /// <returns>
        /// The response object.
        /// </returns>
        public static async Task<HttpResponseMessage> PostAsync(
            this HttpClient client,
            string requestUri,
            object content)
        {
            new { client }.Must().NotBeNull();
            new { requestUri }.Must().NotBeNullNorWhiteSpace();
            new { content }.Must().NotBeNull();

            var contentJson = Serializer.SerializeToString(content);

            var result = await client.PostAsync(requestUri, new StringContent(contentJson, Encoding.UTF8, JsonMediaType));

            return result;
        }

        /// <summary>
        /// Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="client">The http client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="content">The content to send.</param>
        /// <returns>
        /// The response object.
        /// </returns>
        public static async Task<HttpResponseMessage> PutAsync(
            this HttpClient client,
            string requestUri,
            object content)
        {
            new { client }.Must().NotBeNull();
            new { requestUri }.Must().NotBeNullNorWhiteSpace();
            new { content }.Must().NotBeNull();

            var contentJson = Serializer.SerializeToString(content);

            var result = await client.PutAsync(requestUri, new StringContent(contentJson, Encoding.UTF8, JsonMediaType));

            return result;
        }
    }
}