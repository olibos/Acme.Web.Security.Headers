// <copyright file="FrameOptions.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    using ComponentModel;

    /// <summary>
    /// The X-Frame-Options HTTP response header can be used to indicate whether or not a browser should be allowed to render a page in a &lt;frame&gt;, &lt;iframe&gt; or &lt;object&gt; .
    /// Sites can use this to avoid clickjacking attacks, by ensuring that their content is not embedded into other sites.
    ///
    /// The added security is only provided if the user accessing the document is using a browser supporting X-Frame-Options.
    /// </summary>
    public enum FrameOptions
    {
        /// <summary>
        /// The X-Frame-Options Header is not emitted.
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// The page cannot be displayed in a frame, regardless of the site attempting to do so.
        /// </summary>
        [HeaderValue("DENY")]
        Deny = 1,

        /// <summary>
        /// The page can only be displayed in a frame on the same origin as the page itself.
        /// </summary>
        [HeaderValue("SAMEORIGIN")]
        SameOrigin = 2
    }
}