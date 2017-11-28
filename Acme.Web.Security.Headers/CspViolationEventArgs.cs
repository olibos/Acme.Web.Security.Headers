// <copyright file="CspViolationEventArgs.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    using System;
    using Acme.Web.Security.Headers.Model;

    /// <summary>
    /// Provides data for any events that occur when a browser refuses to load or execute a resource based on the current CSP.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CspViolationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CspViolationEventArgs"/> class.
        /// </summary>
        /// <param name="report">The report.</param>
        public CspViolationEventArgs(Report report)
        {
            this.Report = report;
        }

        /// <summary>
        /// Gets the report.
        /// </summary>
        /// <value>
        /// The report.
        /// </value>
        public Report Report { get; }
    }
}