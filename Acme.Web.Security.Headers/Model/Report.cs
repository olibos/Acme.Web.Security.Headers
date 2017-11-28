// <copyright file="Report.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A Browser <see cref="Report"/>.
    /// </summary>
    [DataContract(Name = "report")]
    public class Report
    {
        /// <summary>
        /// Gets or sets the CSP.
        /// </summary>
        /// <value>
        /// The CSP.
        /// </value>
        [DataMember(Name = "csp-report")]
        public Csp Csp { get; set; }
    }
}