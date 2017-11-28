// <copyright file="SecuritySection.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Configuration
{
    using System.Configuration;
    using System.Web;

    /// <summary>
    /// <see cref="SecuritySection"/> is the web config section.
    /// </summary>
    /// <seealso cref="ConfigurationSection" />
    public class SecuritySection : ConfigurationSection
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static SecuritySection Instance => ConfigurationManager.GetSection("acme.web.security.headers") as SecuritySection;

        /// <summary>
        /// Gets the content security policy.
        /// </summary>
        /// <value>
        /// The content security policy.
        /// </value>
        [ConfigurationProperty("contentSecurityPolicy", IsRequired = false)]
        public ContentSecurityPolicyConfiguration ContentSecurityPolicy => (ContentSecurityPolicyConfiguration)this["contentSecurityPolicy"];

        /// <summary>
        /// Gets a value indicating whether the content type options header is set.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the content type options header is set; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("contentTypeOptions", IsRequired = false, DefaultValue = true)]
        public bool ContentTypeOptions => (bool)this["contentTypeOptions"];

        /// <summary>
        /// Gets the frame options.
        /// </summary>
        /// <value>
        /// The frame options.
        /// </value>
        [ConfigurationProperty("frameOptions", IsRequired = false, DefaultValue = FrameOptions.Deny)]
        public FrameOptions FrameOptions => (FrameOptions)this["frameOptions"];

        /// <summary>
        /// Gets the XSS protection.
        /// </summary>
        /// <value>
        /// The XSS protection.
        /// </value>
        [ConfigurationProperty("xssProtection", IsRequired = false, DefaultValue = XssProtection.Block)]
        public XssProtection XssProtection => (XssProtection)this["xssProtection"];

        /// <summary>
        /// Gets the XMLNS.
        /// </summary>
        /// <value>
        /// The XMLNS.
        /// </value>
        [ConfigurationProperty("xmlns", IsRequired = false)]
        private string Xmlns => this["xmlns"]?.ToString() ?? string.Empty;

        /// <summary>
        /// Writes the headers to the specified <paramref name="response" />.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="reportUri">The report URI.</param>
        public void WriteHeaders(HttpResponseBase response, string reportUri)
        {
            if (!response.HeadersWritten)
            {
                var csp = this.ContentSecurityPolicy.GetHeaderValue(reportUri);
                if (!string.IsNullOrWhiteSpace(csp))
                {
                    response.AppendHeader("Content-Security-Policy", csp);
                }

                switch (this.XssProtection)
                {
                    case XssProtection.Block:
                        response.AppendHeader("X-XSS-Protection", "1; mode=block");
                        break;

                    case XssProtection.Enabled:
                        response.AppendHeader("X-XSS-Protection", "1");
                        break;
                }

                if (this.ContentTypeOptions)
                {
                    response.AppendHeader("X-Content-Type-Options", "nosniff");
                }

                if (this.FrameOptions != FrameOptions.Disabled)
                {
                    response.AppendHeader("X-Frame-Options", this.FrameOptions.ToString().ToUpperInvariant());
                }
            }
        }
    }
}