// <copyright file="WebSecurityAttribute.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    using System;
    using System.Web.Mvc;
    using Acme.Web.Security.Headers.Api;
    using Acme.Web.Security.Headers.Extensions;
    using Configuration;

    /// <summary>
    /// <see cref="WebSecurityAttribute"/> adds security headers to web pages.
    /// </summary>
    /// <seealso cref="ActionFilterAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class WebSecurityAttribute : ActionFilterAttribute
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
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SecuritySection.Instance?.WriteHeaders(filterContext.HttpContext.Response, HttpConfigurationExtensions.IsCspWebHookEnabled && CspViolation != null ? CspWebHookRoute : null);
        }

        /// <summary>
        /// Called when a browser refuses to load or execute a resource based on the current CSP.
        /// </summary>
        /// <param name="e">The <see cref="CspViolationEventArgs"/> instance containing the event data.</param>
        internal static void OnCspViolation(CspViolationEventArgs e)
            => CspViolation?.Invoke(null, e);
    }
}