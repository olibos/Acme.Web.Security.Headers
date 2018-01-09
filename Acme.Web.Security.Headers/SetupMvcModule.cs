// <copyright file="SetupMvcModule.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    using System.Web;
    using System.Web.Http;
    using Acme.Web.Security.Headers.Api;
    using Configuration;
    using Extensions;
    using HttpConfigurationExtensions = Extensions.HttpConfigurationExtensions;

    /// <summary>
    /// Setup Acme.Web.Security.Headers for an MVC web site.
    /// </summary>
    /// <seealso cref="RemoveInfoHeaders" />
    public class SetupMvcModule : RemoveInfoHeaders
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public override void Init(HttpApplication context)
        {
            base.Init(context);
            GlobalConfiguration.Configure(config => config.RegisterCspWebHook());
        }

        /// <summary>
        /// Occurs just before ASP.NET sends HTTP headers to the client.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void PreSendRequestHeaders(HttpContextBase context)
        {
            base.PreSendRequestHeaders(context);
            SecuritySection.Instance?.WriteHeaders(context, HttpConfigurationExtensions.IsCspWebHookEnabled && CspViolationController.HasCspViolationEventHandler ? CspViolationController.CspWebHookRoute : null);
        }
    }
}