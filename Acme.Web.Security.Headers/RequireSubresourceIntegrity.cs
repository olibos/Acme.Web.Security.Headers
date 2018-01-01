// <copyright file="RequireSubresourceIntegrity.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>

namespace Acme.Web.Security.Headers
{
    using ComponentModel;

    /// <summary>
    /// The HTTP Content-Security-Policy require-sri-for directive instructs the client to require the use of Subresource Integrity for scripts or styles on the page.
    /// </summary>
    public enum RequireSubresourceIntegrity
    {
        /// <summary>
        /// No SRI are required.
        /// </summary>
        None = 0,

        /// <summary>
        /// Requires SRI for scripts.
        /// </summary>
        [HeaderValue("script")]
        Script = 1,

        /// <summary>
        /// Requires SRI for style sheets.
        /// </summary>
        [HeaderValue("style")]
        Style = 2,

        /// <summary>
        /// Requires SRI for both, scripts and style sheets.
        /// </summary>
        [HeaderValue("script style")]
        ScriptAndStyle = 3
    }
}
