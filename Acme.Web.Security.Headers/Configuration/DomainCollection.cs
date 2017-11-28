// <copyright file="DomainCollection.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Configuration
{
    using System.Configuration;
    using System.Diagnostics;
    using System.Text;
    using Extensions;

    /// <summary>
    /// Collection of white listed <see cref="Domain"/>s.
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationElementCollection" />
    [ConfigurationCollection(typeof(Domain), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    [DebuggerDisplay("{HeaderValue}")]
    public class DomainCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets the header value.
        /// </summary>
        /// <value>
        /// The header value.
        /// </value>
        public string HeaderValue
        {
            get
            {
                var buffer = new StringBuilder();
                this.GetHeaderValue(buffer);
                return buffer.ToString();
            }
        }

        /// <summary>
        /// Gets the header value.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void GetHeaderValue(StringBuilder buffer)
        {
            foreach (Domain item in this)
            {
                buffer.Append(item.Host).Append(' ');
            }

            buffer.TrimEnd();
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A newly created <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement() => new Domain();

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element) => ((Domain)element).Host;
    }
}