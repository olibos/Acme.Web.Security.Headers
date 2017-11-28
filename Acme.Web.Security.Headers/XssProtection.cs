// <copyright file="XssProtection.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    /// <summary>
    /// The HTTP X-XSS-Protection response header is a feature of Internet Explorer, Chrome and Safari that stops pages from loading when they detect reflected cross-site scripting (XSS) attacks. Although these protections are largely unnecessary in modern browsers when sites implement a strong Content-Security-Policy that disables the use of inline JavaScript ('unsafe-inline'), they can still provide protections for users of older web browsers that don't yet support CSP.
    /// </summary>
    public enum XssProtection
    {
        /// <summary>
        /// Disables XSS filtering.
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// Enables XSS filtering (usually default in browsers). If a cross-site scripting attack is detected, the browser will sanitize the page (remove the unsafe parts).
        /// </summary>
        Enabled = 1,

        /// <summary>
        /// Enables XSS filtering. Rather than sanitizing the page, the browser will prevent rendering of the page if an attack is detected.
        /// </summary>
        Block = 2
    }
}