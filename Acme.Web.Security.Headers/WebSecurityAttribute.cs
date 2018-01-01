// <copyright file="WebSecurityAttribute.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    using System;
    using System.Web.Mvc;
    using Api;

    /// <summary>
    /// <see cref="WebSecurityAttribute"/> adds security headers to web pages.
    /// </summary>
    /// <seealso cref="ActionFilterAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [Obsolete("Those actions are now made in SetupMvcModule")]
    public sealed class WebSecurityAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The CSP route template.
        /// </summary>
        public const string CspWebHookRoute = CspViolationController.CspWebHookRoute;

        /// <summary>
        /// Occurs when a browser refuses to load or execute a resource based on the current CSP.
        /// </summary>
        [Obsolete("Use SetupMvcModule.CspViolation")]
        public static event EventHandler<CspViolationEventArgs> CspViolation
        {
            add { CspViolationController.CspViolation += value; }
            remove { CspViolationController.CspViolation -= value; }
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /* This class will be removed in next major version. */
        }
    }
}