// <copyright file="Csp.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Model
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    /// <summary>
    /// <see cref="Csp"/> Report.
    /// </summary>
    [DataContract(Name = "csp-report")]
    public class Csp
    {
        /// <summary>
        /// Gets or sets the blocked URI.
        /// </summary>
        /// <value>
        /// The blocked URI.
        /// </value>
        [DataMember(Name = "blocked-uri")]
        public Uri BlockedUri { get; set; }

        /// <summary>
        /// Gets or sets the disposition.
        /// </summary>
        /// <value>
        /// The disposition.
        /// </value>
        [DataMember(Name = "disposition")]
        public string Disposition { get; set; }

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>
        /// The document.
        /// </value>
        [DataMember(Name = "document-uri")]
        public Uri Document { get; set; }

        /// <summary>
        /// Gets or sets the effective directive.
        /// </summary>
        /// <value>
        /// The effective directive.
        /// </value>
        [DataMember(Name = "effective-directive")]
        public string EffectiveDirective { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        [DataMember(Name = "line-number")]
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the original policy.
        /// </summary>
        /// <value>
        /// The original policy.
        /// </value>
        [DataMember(Name = "original-policy")]
        public string OriginalPolicy { get; set; }

        /// <summary>
        /// Gets or sets the referrer.
        /// </summary>
        /// <value>
        /// The referrer.
        /// </value>
        [DataMember(Name = "referrer")]
        public Uri Referrer { get; set; }

        /// <summary>
        /// Gets or sets the script sample.
        /// </summary>
        /// <value>
        /// The script sample.
        /// </value>
        [DataMember(Name = "script-sample")]
        public string ScriptSample { get; set; }

        /// <summary>
        /// Gets or sets the source file.
        /// </summary>
        /// <value>
        /// The source file.
        /// </value>
        [DataMember(Name = "source-file")]
        public string SourceFile { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        [DataMember(Name = "status-code")]
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the violated directive.
        /// </summary>
        /// <value>
        /// The violated directive.
        /// </value>
        [DataMember(Name = "violated-directive")]
        public string ViolatedDirective { get; set; }
    }
}