// <copyright file="GlobalFilterCollectionExtensions.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Extensions
{
    using System.Web.Mvc;

    /// <summary>
    /// <see cref="GlobalFilterCollectionExtensions"/> contains methods which extends <see cref="GlobalFilterCollection"/>.
    /// </summary>
    public static class GlobalFilterCollectionExtensions
    {
        /// <summary>
        /// Registers the web security filter.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterAcmeWebSecurity(this GlobalFilterCollection filters)
        {
            filters.Add(new WebSecurityAttribute(), int.MinValue);
        }
    }
}