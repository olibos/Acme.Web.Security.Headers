// <copyright file="CspViolationController.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>
namespace Acme.Web.Security.Headers.Api
{
    using System.Web.Http;
    using Model;

    /// <summary>
    /// <see cref="CspViolationController"/> process CSP reports.
    /// </summary>
    /// <seealso cref="ApiController" />
    public class CspViolationController : ApiController
    {
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

            WebSecurityAttribute.OnCspViolation(new CspViolationEventArgs(report));
        }
    }
}