// <copyright file="HeaderValueAttribute.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>

namespace Acme.Web.Security.Headers.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the header value of an <see cref="Enum"/>.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class HeaderValueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderValueAttribute"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public HeaderValueAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; }

        /// <summary>
        /// Gets the header value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="headerValue">The header value.</param>
        /// <returns>The corresponding header value.</returns>
        public static string GetHeaderValue<T>(T headerValue)
            where T : struct, IConvertible
            => Cache<T>.Values.TryGetValue(headerValue, out var value) ? value : null;

        /// <summary>
        /// Cached header values.
        /// </summary>
        /// <typeparam name="T">Type of Enum</typeparam>
        private static class Cache<T>
        {
            /// <summary>
            /// Gets the values.
            /// </summary>
            /// <value>
            /// The values.
            /// </value>
            public static Dictionary<T, string> Values { get; } = Initialize();

            /// <summary>
            /// Initializes the cache.
            /// </summary>
            /// <returns>The cached values</returns>
            private static Dictionary<T, string> Initialize()
            {
                var enumType = typeof(T);
                if (!enumType.IsEnum)
                {
                    throw new ArgumentException("T must be an enum type");
                }

                var values = enumType.GetEnumValues();
                var result = new Dictionary<T, string>(values.Length);
                foreach (int value in values)
                {
                    var headerValue = (HeaderValueAttribute)enumType.GetMember(enumType.GetEnumName(value))[0]
                                            .GetCustomAttributes(typeof(HeaderValueAttribute), false)
                                            .FirstOrDefault();
                    if (headerValue != null)
                    {
                        result.Add((T)(object)value, headerValue.Value);
                    }
                }

                return result;
            }
        }
    }
}