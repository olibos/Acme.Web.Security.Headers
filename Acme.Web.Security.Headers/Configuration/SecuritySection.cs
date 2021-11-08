// <copyright file="SecuritySection.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Configuration
{
    using System;
    using System.Configuration;
    using System.Net.Mime;
    using System.Web;
    using static ComponentModel.HeaderValueAttribute;

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
        [ConfigurationProperty("frameOptions", IsRequired = false, DefaultValue = FrameOptions.Disabled)]
        public FrameOptions FrameOptions => (FrameOptions)this["frameOptions"];

        /// <summary>
        /// Gets the referrer policy.
        /// </summary>
        /// <value>
        /// The referrer policy.
        /// </value>
        [ConfigurationProperty("referrerPolicy", IsRequired = false, DefaultValue = ReferrerPolicy.Disabled)]
        public ReferrerPolicy ReferrerPolicy => (ReferrerPolicy)this["referrerPolicy"];

        /// <summary>
        /// Gets a value indicating whether the report media type must be registered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the report media type must be registered; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("registerReportMediaType", IsRequired = false, DefaultValue = true)]
        public bool RegisterReportMediaType => (bool)this["registerReportMediaType"];

        /// <summary>
        /// Gets the content security policy.
        /// </summary>
        /// <value>
        /// The content security policy.
        /// </value>
        [ConfigurationProperty("strictTransportSecurity", IsRequired = false)]
        public StrictTransportSecurityConfiguration StrictTransportSecurity => (StrictTransportSecurityConfiguration)this["strictTransportSecurity"];

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
        /// Writes the headers to the specified <paramref name="context" />.Response.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="reportUri">The report URI.</param>
        public void WriteHeaders(HttpContextBase context, string reportUri)
        {
            var response = context.Response;
            if (response.HeadersWritten)
            {
                return;
            }

            this.WriteHsts(context);
            if (!MediaTypeNames.Text.Html.Equals(response.ContentType, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            AppendHeader(response, HeaderNames.ContentSecurityPolicy, this.ContentSecurityPolicy.GetHeaderValue(reportUri));
            AppendHeader(response, HeaderNames.XssProtection, GetHeaderValue(this.XssProtection));
            AppendHeader(response, HeaderNames.ReferrerPolicy, GetHeaderValue(this.ReferrerPolicy));
            AppendHeader(response, HeaderNames.FrameOptions, GetHeaderValue(this.FrameOptions));
            if (this.ContentTypeOptions)
            {
                response.AppendHeader(HeaderNames.ContentTypeOptions, "nosniff");
            }
        }

        /// <summary>
        /// Appends the header.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="headerName">Name of the header.</param>
        /// <param name="headerValue">The header value.</param>
        private static void AppendHeader(HttpResponseBase response, string headerName, string headerValue)
        {
            if (!string.IsNullOrEmpty(response.Headers[headerName]) || string.IsNullOrWhiteSpace(headerValue))
            {
                return;
            }

            response.AppendHeader(headerName, headerValue);
        }

        /// <summary>
        /// Writes the HSTS.
        /// </summary>
        /// <param name="context">The context.</param>
        private void WriteHsts(HttpContextBase context)
        {
            var request = context.Request;
            if (this.StrictTransportSecurity.MaxAge <= 0 || // Disable if max age is not a positive number.
                !request.IsSecureConnection || // HSTS should be added only with HTTPS.
                !request.Url.IsDefaultPort /* Avoid issue with IIS Express custom HTTPS port. */)
            {
                return;
            }

            AppendHeader(context.Response, HeaderNames.StrictTransportSecurity, this.StrictTransportSecurity.HeaderValue);
        }
    }
}