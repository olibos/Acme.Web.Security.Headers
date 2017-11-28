// <copyright file="AcmeConfig.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace $rootnamespace$
{
    using System.Web.Http;
    using System.Web.Mvc;
    using Acme.Web.Security.Headers;
    using Acme.Web.Security.Headers.Extensions;

    /// <summary>
    /// Setup ACME Web Security Headers.
    /// </summary>
    public static class AcmeConfig
    {
        /// <summary>
        /// Registers <see cref="WebSecurityAttribute"/> as global filter.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.RegisterAcmeWebSecurity();
        }

        /// <summary>
        /// Registers the web API.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void RegisterWebApi(HttpConfiguration config)
        {
            config.RegisterCspWebHook();
            WebSecurityAttribute.CspViolation += OnCspViolation;
        }

        /// <summary>
        /// Called when a browser refuses to load or execute a resource based on the current CSP.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CspViolationEventArgs"/> instance containing the event data.</param>
        private static void OnCspViolation(object sender, CspViolationEventArgs e)
        {
            /*
             * A Browser reports a CSP violation.
             * You can log it, sent it by mail, ...
             */
        }
    }
}