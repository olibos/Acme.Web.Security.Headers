// <copyright file="StringBuilderExtensions.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Extensions
{
    using System.Text;

    /// <summary>
    /// <see cref="StringBuilderExtensions"/> contains methods which extends <see cref="StringBuilder"/>.
    /// </summary>
    internal static class StringBuilderExtensions
    {
        /// <summary>
        /// Trims the end.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The <see cref="StringBuilder"/>.</returns>
        public static StringBuilder TrimEnd(this StringBuilder builder)
        {
            int count = 0;
            for (int i = builder.Length - 1; i >= 0 && builder[i] == ' '; i--)
            {
                count++;
            }

            if (count > 0)
            {
                builder.Length -= count;
            }

            return builder;
        }
    }
}