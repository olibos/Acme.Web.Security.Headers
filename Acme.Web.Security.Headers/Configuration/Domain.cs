// <copyright file="Domain.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Configuration
{
    using System.Configuration;

    /// <summary>
    /// <see cref="Domain"/> represents a domain or a wildcard domain or event a scheme+domain.
    /// </summary>
    /// <seealso cref="ConfigurationElement" />
    public class Domain : ConfigurationElement
    {
        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        [ConfigurationProperty("host", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
        public string Host => (string)this["host"];
    }
}