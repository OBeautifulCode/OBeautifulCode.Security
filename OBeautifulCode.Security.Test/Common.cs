// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Test
{
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "This is a good name for the class.")]
    public static class Common
    {
        public static string RemoveLineBreaks(
            this string value)
        {
            var result = Regex.Replace(value, @"\r\n?|\n", string.Empty);
            return result;
        }
    }
}
