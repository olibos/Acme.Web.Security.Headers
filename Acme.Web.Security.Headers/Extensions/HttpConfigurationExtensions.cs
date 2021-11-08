// <copyright file="HttpConfigurationExtensions.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Extensions
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Acme.Web.Security.Headers.Configuration;

    using Api;

    /// <summary>
    /// <see cref="HttpConfigurationExtensions"/> contains methods which extends <see cref="HttpConfiguration"/>.
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        /// The route name.
        /// </summary>
        private const string RouteName = "Acme.Web.Security.Headers.Csp";

        /// <summary>
        /// Gets a value indicating whether CSP web hook is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if CSP web hook is enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool IsCspWebHookEnabled { get; private set; }

        /// <summary>
        /// Sends the CSP error by mail.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterCspWebHook(this HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (SecuritySection.Instance.RegisterReportMediaType && !configuration.Formatters.Any(f => f.SupportedMediaTypes.Any(m => m.MediaType == CspReportMediaTypeFormatter.MediaType)))
            {
                configuration.Formatters.Add(new CspReportMediaTypeFormatter());
            }

            if (!configuration.Routes.ContainsKey(RouteName))
            {
                IsCspWebHookEnabled = true;
                configuration.Routes.MapHttpRoute(
                    name: RouteName,
                    routeTemplate: CspViolationController.CspWebHookRoute.TrimStart('/'),
                    defaults: new { controller = "CspViolation" });
            }
        }
    }
}