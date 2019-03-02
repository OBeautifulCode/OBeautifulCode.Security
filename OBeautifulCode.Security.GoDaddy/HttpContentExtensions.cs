// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpContentExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.GoDaddy
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Naos.Serialization.Domain;
    using Naos.Serialization.Json;

    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Extension methods on type <see cref="HttpContent"/>.
    /// </summary>
    internal static class HttpContentExtensions
    {
        private static readonly IDeserialize Deserializer = new NaosJsonSerializer(SerializationKind.Minimal);

        /// <summary>
        /// Deserialize the HTTP content to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="content">The HTTP content.</param>
        /// <returns>
        /// The deserialized object.
        /// </returns>
        public static async Task<T> DeserializeAsync<T>(
            this HttpContent content)
        {
            new { content }.Must().NotBeNull();

            var contentJson = await content.ReadAsStringAsync();

            var result = Deserializer.Deserialize<T>(contentJson);

            return result;
        }
    }
}