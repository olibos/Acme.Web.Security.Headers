// <copyright file="InsecureRequests.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    using Acme.Web.Security.Headers.ComponentModel;

    /// <summary>
    /// Determines how user agents handle insecure URLs (those served over HTTP).
    /// </summary>
    public enum InsecureRequests
    {
        /// <summary>
        /// Let the user agents choice what to do with insecure URLs (those served over HTTP).
        /// </summary>
        BrowserDefault = 0,

        /// <summary>
        /// The upgrade-insecure-requests directive instructs user agents to treat all of a site's insecure URLs (those served over HTTP) as though they have been replaced with secure URLs (those served over HTTPS). This directive is intended for web sites with large numbers of insecure legacy URLs that need to be rewritten.
        /// The upgrade-insecure-requests directive will not ensure that users visiting your site via links on third-party sites will be upgraded to HTTPS for the top-level navigation and thus does not replace the Strict-Transport-Security (HSTS) header, which should still be set with an appropriate max-age to ensure that users are not subject to SSL stripping attacks.
        /// </summary>
        [HeaderValue("upgrade-insecure-requests")]
        UpgradeInsecureRequests = 1,

        /// <summary>
        /// The upgrade-insecure-requests directive instructs user agents to treat all of a site's insecure URLs (those served over HTTP) as though they have been replaced with secure URLs (those served over HTTPS). This directive is intended for web sites with large numbers of insecure legacy URLs that need to be rewritten.
        /// All mixed content resource requests are blocked, including both active and passive mixed content. This also applies to &lt;iframe&gt; documents, ensuring the entire page is mixed content free.
        /// </summary>
        [HeaderValue("block-all-mixed-content")]
        BlockAllMixedContent = 2
    }
}