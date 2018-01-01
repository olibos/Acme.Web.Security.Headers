// <copyright file="HeaderNames.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>

namespace Acme.Web.Security.Headers
{
    /// <summary>
    /// Names of security headers.
    /// </summary>
    public static class HeaderNames
    {
        /// <summary>
        /// The content security policy.
        /// </summary>
        public const string ContentSecurityPolicy = "Content-Security-Policy";

        /// <summary>
        /// The content type options.
        /// </summary>
        public const string ContentTypeOptions = "X-Content-Type-Options";

        /// <summary>
        /// The frame options.
        /// </summary>
        public const string FrameOptions = "X-Frame-Options";

        /// <summary>
        /// The referrer policy.
        /// </summary>
        public const string ReferrerPolicy = "Referrer-Policy";

        /// <summary>
        /// The strict transport security.
        /// </summary>
        public const string StrictTransportSecurity = "Strict-Transport-Security";

        /// <summary>
        /// The XSS protection
        /// </summary>
        public const string XssProtection = "X-XSS-Protection";
    }
}