// <copyright file="CspViolationController.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Api
{
    using System;
    using System.Web.Http;
    using Model;

    /// <summary>
    /// <see cref="CspViolationController"/> process CSP reports.
    /// </summary>
    /// <seealso cref="ApiController" />
    public class CspViolationController : ApiController
    {
        /// <summary>
        /// The CSP route template.
        /// </summary>
        public const string CspWebHookRoute = "/$acme$/csp";

        /// <summary>
        /// Occurs when a browser refuses to load or execute a resource based on the current CSP.
        /// </summary>
        public static event EventHandler<CspViolationEventArgs> CspViolation;

        /// <summary>
        /// Gets a value indicating whether a CSP violation event handler is registered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if a CSP violation event handler is registered; otherwise, <c>false</c>.
        /// </value>
        public static bool HasCspViolationEventHandler => CspViolation != null;

        /// <summary>
        /// Posts the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        public void Post([FromBody]Report report)
        {
            if (report?.Csp == null)
            {
                return;
            }

            CspViolation?.Invoke(null, new CspViolationEventArgs(report));
        }
    }
}