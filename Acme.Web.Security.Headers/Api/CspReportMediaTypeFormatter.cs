// <copyright file="CspReportMediaTypeFormatter.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Api
{
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    /// <summary>
    /// <see cref="CspReportMediaTypeFormatter"/> read CSP reports.
    /// </summary>
    /// <seealso cref="System.Net.Http.Formatting.JsonMediaTypeFormatter" />
    public class CspReportMediaTypeFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// The media type.
        /// </summary>
        public const string MediaType = "application/csp-report";

        /// <summary>
        /// Initializes a new instance of the <see cref="CspReportMediaTypeFormatter"/> class.
        /// </summary>
        public CspReportMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Clear();
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(MediaType));
        }
    }
}