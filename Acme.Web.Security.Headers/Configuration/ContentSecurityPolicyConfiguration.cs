// <copyright file="ContentSecurityPolicyConfiguration.cs" company="ACME">
// Copyright (c) ACME. All rights reserved.
// </copyright>
// <author>Olivier Bossaer</author>

namespace Acme.Web.Security.Headers.Configuration
{
    using System.Configuration;
    using System.Diagnostics;
    using System.Text;
    using Acme.Web.Security.Headers.ComponentModel;
    using Extensions;

    /// <summary>
    /// The Content-Security-Policy HTTP response header helps you reduce XSS risks on modern browsers by declaring what dynamic resources are allowed to load via a HTTP Header.
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationElement" />
    [DebuggerDisplay("{HeaderValue}")]
    public class ContentSecurityPolicyConfiguration : ConfigurationElement
    {
        /// <summary>
        /// Gets the child.
        /// Defines valid sources for web workers and nested browsing contexts loaded using elements such as &lt;frame&gt; and &lt;iframe&gt;
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        [ConfigurationProperty("child", IsRequired = false)]
        public CspDirectiveConfiguration Child => (CspDirectiveConfiguration)this["child"];

        /// <summary>
        /// Gets the connect.
        /// Applies to XMLHttpRequest (AJAX), WebSocket or EventSource. If not allowed the browser emulates a 400 HTTP status code.
        /// </summary>
        /// <value>
        /// The connect.
        /// </value>
        [ConfigurationProperty("connect", IsRequired = false)]
        public CspDirectiveConfiguration Connect => (CspDirectiveConfiguration)this["connect"];

        /// <summary>
        /// Gets the default-src, The default-src is the default policy for loading content such as JavaScript, Images, CSS, Fonts, AJAX requests, Frames, HTML5 Media..
        /// </summary>
        /// <value>
        /// The default-src.
        /// </value>
        [ConfigurationProperty("default", IsRequired = false)]
        public CspDirectiveConfiguration Default => (CspDirectiveConfiguration)this["default"];

        /// <summary>
        /// Gets the font.
        /// Defines valid sources of fonts.
        /// </summary>
        /// <value>
        /// The font.
        /// </value>
        [ConfigurationProperty("font", IsRequired = false)]
        public CspDirectiveConfiguration Font => (CspDirectiveConfiguration)this["font"];

        /// <summary>
        /// Gets the form.
        /// Defines valid sources that can be used as a HTML &lt;form&gt; action.
        /// </summary>
        /// <value>
        /// The form.
        /// </value>
        [ConfigurationProperty("form", IsRequired = false)]
        public CspDirectiveConfiguration Form => (CspDirectiveConfiguration)this["form"];

        /// <summary>
        /// Gets the frame-src, it specifies valid sources for nested browsing contexts loading using elements such as &lt;frame&gt; and &lt;iframe&gt;..
        /// </summary>
        /// <value>
        /// The frame.
        /// </value>
        [ConfigurationProperty("frame", IsRequired = false)]
        public CspDirectiveConfiguration Frame => (CspDirectiveConfiguration)this["frame"];

        /// <summary>
        /// Gets the frame ancestors, it specifies valid parents that may embed a page using &lt;frame&gt;, &lt;iframe&gt;, &lt;object&gt;, &lt;embed&gt;, or &lt;applet&gt;.
        /// </summary>
        /// <value>
        /// The frame ancestors.
        /// </value>
        [ConfigurationProperty("frameAncestors", IsRequired = false)]
        public CspDirectiveConfiguration FrameAncestors => (CspDirectiveConfiguration)this["frameAncestors"];

        /// <summary>
        /// Gets the header value.
        /// </summary>
        /// <value>
        /// The header value.
        /// </value>
        public string HeaderValue => this.GetHeaderValue(null);

        /// <summary>
        /// Gets the img.
        /// Defines valid sources of images.
        /// </summary>
        /// <value>
        /// The img.
        /// </value>
        [ConfigurationProperty("img", IsRequired = false)]
        public CspDirectiveConfiguration Img => (CspDirectiveConfiguration)this["img"];

        /// <summary>
        /// Gets the manifest, it specifies valid sources of application manifest files.
        /// </summary>
        /// <value>
        /// The manifest.
        /// </value>
        [ConfigurationProperty("manifest", IsRequired = false)]
        public CspDirectiveConfiguration Manifest => (CspDirectiveConfiguration)this["manifest"];

        /// <summary>
        /// Gets the media.
        /// Defines valid sources of audio and video, eg HTML5 &lt;audio&gt;, &lt;video&gt; elements.
        /// </summary>
        /// <value>
        /// The media.
        /// </value>
        [ConfigurationProperty("media", IsRequired = false)]
        public CspDirectiveConfiguration Media => (CspDirectiveConfiguration)this["media"];

        /// <summary>
        /// Gets the object.
        /// Defines valid sources of plugins, eg &lt;object&gt;, &lt;embed&gt; or &lt;applet&gt;.
        /// </summary>
        /// <value>
        /// The object.
        /// </value>
        [ConfigurationProperty("object", IsRequired = false)]
        public CspDirectiveConfiguration Object => (CspDirectiveConfiguration)this["object"];

        /// <summary>
        /// Gets the require subresource integrity.
        /// </summary>
        /// <value>
        /// The require subresource integrity.
        /// </value>
        [ConfigurationProperty("requireSubresourceIntegrity", IsRequired = false, DefaultValue = RequireSubresourceIntegrity.None)]
        public RequireSubresourceIntegrity RequireSubresourceIntegrity => (RequireSubresourceIntegrity)this["requireSubresourceIntegrity"];

        /// <summary>
        /// Gets the script.
        /// Defines valid sources of JavaScript.
        /// </summary>
        /// <value>
        /// The script.
        /// </value>
        [ConfigurationProperty("script", IsRequired = false)]
        public CspDirectiveConfiguration Script => (CspDirectiveConfiguration)this["script"];

        /// <summary>
        /// Gets the style.
        /// Defines valid sources of stylesheets.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        [ConfigurationProperty("style", IsRequired = false)]
        public CspDirectiveConfiguration Style => (CspDirectiveConfiguration)this["style"];

        /// <summary>
        /// Gets the worker, it specifies valid sources for Worker, SharedWorker, or ServiceWorker scripts.
        /// </summary>
        /// <value>
        /// The worker.
        /// </value>
        [ConfigurationProperty("worker", IsRequired = false)]
        public CspDirectiveConfiguration Worker => (CspDirectiveConfiguration)this["worker"];

        /// <summary>
        /// Gets the header value.
        /// </summary>
        /// <param name="reportUri">The report URI.</param>
        /// <returns>The header value.</returns>
        public string GetHeaderValue(string reportUri)
        {
            var buffer = new StringBuilder();
            TryAddSection(buffer, "default-src", this.Default);
            TryAddSection(buffer, "child-src", this.Child);
            TryAddSection(buffer, "connect-src", this.Connect);
            TryAddSection(buffer, "font-src", this.Font);
            TryAddSection(buffer, "form-action", this.Form);
            TryAddSection(buffer, "frame-src", this.Frame);
            TryAddSection(buffer, "frame-ancestors", this.FrameAncestors);
            TryAddSection(buffer, "img-src", this.Img);
            TryAddSection(buffer, "manifest-src", this.Manifest);
            TryAddSection(buffer, "media-src", this.Media);
            TryAddSection(buffer, "object-src", this.Object);
            TryAddSection(buffer, "require-sri-for", HeaderValueAttribute.GetHeaderValue(this.RequireSubresourceIntegrity));
            TryAddSection(buffer, "script-src", this.Script);
            TryAddSection(buffer, "style-src", this.Style);
            TryAddSection(buffer, "worker-src", this.Worker);
            if (buffer.Length > 0)
            {
                TryAddSection(buffer, "report-uri", reportUri);
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Tries to add the specified section.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="sectionConfiguration">The section configuration.</param>
        private static void TryAddSection(StringBuilder buffer, string sectionName, CspDirectiveConfiguration sectionConfiguration)
        {
            buffer.Append(sectionName);
            int position = buffer.Length;
            sectionConfiguration.GetHeaderValue(buffer.Append(' '));
            if (buffer.TrimEnd().Length != position)
            {
                buffer.Append(';');
            }
            else
            {
                // There is no settings for this section -> cleanup!
                buffer.Length -= sectionName.Length;
            }
        }

        /// <summary>
        /// Tries to add the specified section.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="sectionConfiguration">The section configuration.</param>
        private static void TryAddSection(StringBuilder buffer, string sectionName, string sectionConfiguration)
        {
            if (!string.IsNullOrEmpty(sectionConfiguration))
            {
                buffer.Append(sectionName)
                      .Append(' ')
                      .Append(sectionConfiguration)
                      .Append(';');
            }
        }
    }
}