// <copyright file="CspDirectiveConfiguration.cs" company="ACME">
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
    /// The Content-Security-Policy header value is made up of one or more directives (defined below), multiple directives are separated with a semicolon ;
    /// </summary>
    /// <seealso cref="ConfigurationElement" />
    [DebuggerDisplay("{HeaderValue}")]
    public class CspDirectiveConfiguration : ConfigurationElement
    {
        /// <summary>
        /// Gets a value indicating whether the page allows any URL except data: blob: filesystem: schemes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page allows any URL except data: blob: filesystem: schemes; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("all", IsRequired = false, DefaultValue = false)]
        public bool All => (bool)this["all"];

        /// <summary>
        /// Gets a value indicating whether the page allows loading resources over HTTP on any domain.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page allows loading resources over HTTP on any domain; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("http", IsRequired = false, DefaultValue = false)]
        public bool AllowAllHttp => (bool)this["http"];

        /// <summary>
        /// Gets a value indicating whether the page allows loading resources over HTTPS on any domain.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page allows loading resources over HTTPS on any domain; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("https", IsRequired = false, DefaultValue = false)]
        public bool AllowAllHttps => (bool)this["https"];

        /// <summary>
        /// Gets a value indicating whether the page allows use of inline source elements such as style attribute, onclick, or script tag bodies (depends on the context of the source it is applied to) and javascript: URIs.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page allows use of inline source elements; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("data", IsRequired = false, DefaultValue = false)]
        public bool AllowDataUri => (bool)this["data"];

        /// <summary>
        /// Gets a value indicating whether the page allows loading resources from the same origin (same scheme, host and port).
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page allows loading resources from the same origin; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("self", IsRequired = false, DefaultValue = false)]
        public bool AllowSelf => (bool)this["self"];

        /// <summary>
        /// Gets a value indicating whether the page allows use of inline source elements such as style attribute, onclick, or script tag bodies (depends on the context of the source it is applied to) and javascript: URIs.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page allows use of inline source elements; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("unsafeInline", IsRequired = false, DefaultValue = false)]
        public bool AllowUnsafeInline => (bool)this["unsafeInline"];

        /// <summary>
        /// Gets a value indicating whether the page allows unsafe dynamic code evaluation such as JavaScript eval().
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page allows unsafe dynamic code evaluation; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("unsafeEval", IsRequired = false, DefaultValue = false)]
        public bool AllowUnsafeScriptEval => (bool)this["unsafeEval"];

        /// <summary>
        /// Gets a value indicating whether the page allows web sockets connections.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page allows web sockets connections; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("ws", IsRequired = false, DefaultValue = false)]
        public bool AllowWebSockets => (bool)this["ws"];

        /// <summary>
        /// Gets the trusted domains.
        /// </summary>
        /// <value>
        /// The trusted domains.
        /// </value>
        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public DomainCollection Domains => (DomainCollection)this[string.Empty];

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
        /// Gets a value indicating whether the page prevents loading resources from any source.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the page prevents loading resources from any source; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("none", IsRequired = false, DefaultValue = false)]
        public bool None => (bool)this["none"];

        /// <summary>
        /// Gets the header value.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void GetHeaderValue(StringBuilder buffer)
        {
            if (this.None)
            {
                buffer.Append("'none'");
            }
            else if (this.All)
            {
                buffer.Append("*");
            }
            else
            {
                this.BuildSection(buffer);
            }

            buffer.TrimEnd();
        }

        /// <summary>
        /// Builds the section.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        private void BuildSection(StringBuilder buffer)
        {
            if (this.AllowSelf)
            {
                buffer.Append("'self' ");
            }

            if (this.AllowUnsafeInline)
            {
                buffer.Append("'unsafe-inline' ");
            }

            if (this.AllowUnsafeScriptEval)
            {
                buffer.Append("'unsafe-eval' ");
            }

            if (this.AllowDataUri)
            {
                buffer.Append("data: ");
            }

            if (this.AllowAllHttps)
            {
                buffer.Append("https: ");
            }

            if (this.AllowAllHttp)
            {
                buffer.Append("http: ");
            }

            if (this.AllowWebSockets)
            {
                buffer.Append("ws: ");
            }

            this.Domains.GetHeaderValue(buffer);
        }
    }
}