// <copyright file="RemoveInfoHeaders.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers
{
    using System;
    using System.Web;

    /// <summary>
    /// Remove informative ASP.Net &amp; IIS headers.
    /// </summary>
    /// <seealso cref="System.Web.IHttpModule" />
    public class RemoveInfoHeaders : IHttpModule
    {
        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public virtual void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += PreSendRequestHeaders;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Nothing to dispose.
        }

        /// <summary>
        /// Pres the send request headers.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private static void PreSendRequestHeaders(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var response = application.Context.Response;
            TryRemoveHeader(response, "Server");
            TryRemoveHeader(response, "X-Powered-By");
            TryRemoveHeader(response, "X-AspNet-Version");
            TryRemoveHeader(response, "X-AspNetMvc-Version");
        }

        /// <summary>
        /// Tries to remove the specified header.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="headerName">Name of the header.</param>
        private static void TryRemoveHeader(HttpResponse response, string headerName)
        {
            try
            {
                response.Headers.Remove(headerName);
            }
            catch
            {
                // Best effort, if it fails then too bad ;-)
            }
        }
    }
}