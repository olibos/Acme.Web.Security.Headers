// <copyright file="SetupMvcModule.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using Acme.Web.Security.Headers.Extensions;

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
            GlobalFilters.Filters.RegisterAcmeWebSecurity();
        }
    }
}