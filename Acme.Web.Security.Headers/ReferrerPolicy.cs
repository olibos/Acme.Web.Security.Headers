// <copyright file="ReferrerPolicy.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    using ComponentModel;

    /// <summary>
    /// The Referrer-Policy HTTP header governs which referrer information, sent in the Referer header, should be included with requests made.
    /// </summary>
    public enum ReferrerPolicy
    {
        /// <summary>
        /// No Referrer-Policy HTTP header is emitted.
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// The Referer header will be omitted entirely. No referrer information is sent along with requests.
        /// </summary>
        [HeaderValue("no-referrer")]
        NoReferrer = 1,

        /// <summary>
        /// This is the user agent's default behavior if no policy is specified. The origin is sent as referrer to a-priori as-much-secure destination (HTTPS->HTTPS), but isn't sent to a less secure destination (HTTPS->HTTP).
        /// </summary>
        [HeaderValue("no-referrer-when-downgrade")]
        NoReferrerWhenDowngrade = 2,

        /// <summary>
        /// Only send the origin of the document as the referrer in all cases.
        /// The document https://example.com/page.html will send the referrer https://example.com/.
        /// </summary>
        [HeaderValue("origin")]
        Origin = 3,

        /// <summary>
        /// Send a full URL when performing a same-origin request, but only send the origin of the document for other cases.
        /// </summary>
        [HeaderValue("origin-when-cross-origin")]
        OriginWhenCrossOrigin = 4,

        /// <summary>
        /// A referrer will be sent for same-site origins, but cross-origin requests will contain no referrer information.
        /// </summary>
        [HeaderValue("same-origin")]
        SameOrigin = 5,

        /// <summary>
        /// Only send the origin of the document as the referrer to a-priori as-much-secure destination (HTTPS->HTTPS), but don't send it to a less secure destination (HTTPS->HTTP).
        /// </summary>
        [HeaderValue("strict-origin")]
        StrictOrigin = 6,

        /// <summary>
        /// Send a full URL when performing a same-origin request, only send the origin of the document to a-priori as-much-secure destination (HTTPS->HTTPS), and send no header to a less secure destination (HTTPS->HTTP).
        /// </summary>
        [HeaderValue("strict-origin-when-cross-origin")]
        StrictOriginWhenCrossOrigin = 7,

        /// <summary>
        /// Send a full URL when performing a same-origin or cross-origin request.
        /// </summary>
        /// <remarks>This policy will leak origins and paths from TLS-protected resources to insecure origins. Carefully consider the impact of this setting.</remarks>
        [HeaderValue("unsafe-url")]
        UnsafeUrl = 8
    }
}