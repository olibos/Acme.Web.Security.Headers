// <copyright file="StrictTransportSecurityConfiguration.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Configuration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;

    /// <summary>
    /// The HTTP Strict-Transport-Security response header (often abbreviated as HSTS)  lets a web site tell browsers that it should only be accessed using HTTPS, instead of using HTTP.
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationElement" />
    [DebuggerDisplay("{HeaderValue}")]
    public class StrictTransportSecurityConfiguration : ConfigurationElement
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
                var parameters = new List<string>(3)
                {
                    $"max-age={this.MaxAge}"
                };

                if (this.IncludeSubDomains)
                {
                    parameters.Add("includeSubDomains");
                }

                if (this.Preload)
                {
                    parameters.Add("preload");
                }

                return string.Join("; ", parameters);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this rule applies to all of the site's subdomains as well.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this rule applies to all of the site's subdomains as well; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("includeSubDomains", IsRequired = false, DefaultValue = false)]
        public bool IncludeSubDomains => (bool)this["includeSubDomains"];

        /// <summary>
        /// Gets the time, in seconds, that the browser should remember that a site is only to be accessed using HTTPS.
        /// </summary>
        /// <value>
        /// The maximum age.
        /// </value>
        [ConfigurationProperty("maxAge", IsRequired = true)]
        public int MaxAge => (int)this["maxAge"];

        /// <summary>
        /// Gets a value indicating whether this domain should be added in the preload list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this domain should be added in the preload list; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("preload", IsRequired = false, DefaultValue = false)]
        public bool Preload => (bool)this["preload"];
    }
}