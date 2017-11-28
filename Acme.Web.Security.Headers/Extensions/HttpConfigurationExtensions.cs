// <copyright file="HttpConfigurationExtensions.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Extensions
{
    using System.Linq;
    using System.Web.Http;
    using Api;

    /// <summary>
    /// <see cref="HttpConfigurationExtensions"/> contains methods which extends <see cref="HttpConfiguration"/>.
    /// </summary>
    public static class HttpConfigurationExtensions
    {
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
            if (!configuration.Formatters.Any(f => f.SupportedMediaTypes.Any(m => m.MediaType == CspReportMediaTypeFormatter.MediaType)))
            {
                configuration.Formatters.Add(new CspReportMediaTypeFormatter());
            }

            IsCspWebHookEnabled = true;
            configuration.Routes.MapHttpRoute(
                name: "Acme.Web.Security.Headers.Csp",
                routeTemplate: WebSecurityAttribute.CspWebHookRoute.TrimStart('/'),
                defaults: new { controller = "CspViolation" });
        }
    }
}