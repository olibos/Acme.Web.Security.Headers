Acme.Web.Security.Headers
=========================
Secure your web site/application with a simple package.

[![NuGet Package](https://img.shields.io/nuget/v/Acme.Web.Security.Headers.svg)](https://www.nuget.org/packages/Acme.Web.Security.Headers/) [![NuGet Package](https://img.shields.io/nuget/dt/Acme.Web.Security.Headers.svg)](https://www.nuget.org/packages/Acme.Web.Security.Headers/) [![acme MyGet Build Status](https://www.myget.org/BuildSource/Badge/acme?identifier=952915d4-9b18-4dc1-8a94-dc2b3af997e7)](https://www.myget.org/) [![License](https://img.shields.io/badge/license-LGPL--3.0-blue.svg)](LICENSE) 

Getting started
---------------

Install [NuGet Package](https://www.nuget.org/packages/Acme.Web.Security.Headers/) Acme.Web.Security.Headers from Package Manager or from Package Manager Console:
```
PM> Install-Package Acme.Web.Security.Headers
```

After installation your web.config has a new configuration section with those default values:
```xml
<configuration>
  <acme.web.security.headers xmlns="Acme.Web.Security.Headers" referrerPolicy="StrictOriginWhenCrossOrigin" frameOptions="Deny">
    <contentSecurityPolicy>
      <default self="true"/>
    </contentSecurityPolicy>
    <strictTransportSecurity maxAge="31536000" includeSubDomains="true" preload="true"/>
  </acme.web.security.headers>
</configuration>
```

The section has a schema to let you have all intellisense and documentation for every property and for every possible values.

Web.config changes
------------------
The package set your cookies to secure by default ([Require SSL](https://www.owasp.org/index.php/SecureFlag) & [HTTP Only](https://www.owasp.org/index.php/HttpOnly))
```xml
<configuration>
  <system.web>
    <httpCookies requireSSL="true" httpOnlyCookies="true" />
  </system.web>
</configuration>
```
If your web site has to work in HTTP you may adapt that configuration but I strongly suggest you to switch and consider free alternatives like [Let’s Encrypt](https://letsencrypt.org/).
If you need some cookies in client side then you should set [HttpCookie.HttpOnly](https://msdn.microsoft.com/en-us/library/system.web.httpcookie.httponly(v=vs.110).aspx)=false in your code.
---
The package remove all sensitive information like Server _Name_, _Powered By_, _ASP.NET & MVC Versions_ (this configuration is also enforced by code)
```xml
<configuration>
  <system.web>
    <httpRuntime enableVersionHeader="false" enableHeaderChecking="true" />
  </system.web>
  <system.webServer>
    <httpProtocol>
      <customHeaders>
        <remove name="Server" />
        <remove name="X-Powered-By" />
        <remove name="X-AspNet-Version" />
        <remove name="X-AspNetMvc-Version" />
      </customHeaders>
    </httpProtocol>
  </system.webServer>
</configuration>
```
> Inspired by "[Shhh… don’t let your response headers talk too loudly](https://www.troyhunt.com/shhh-dont-let-your-response-headers/)" by [Troy Hunt](https://www.troyhunt.com/)

It removes the trace handler (trace.axd) completly.
To avoid "/trace.axd" returning a *500* if not enabled and telling the world you are running an ASP.NET website!
```xml
<configuration>
  <system.webServer>
    <handlers>
      <remove name="TraceHandler-Integrated" />
      <remove name="TraceHandler-Integrated-4.0" />
    </handlers>
  </system.webServer>
</configuration>
```
> Inspired by "[Securing the ASP.NET MVC Web.config](https://rehansaeed.com/securing-the-aspnet-mvc-web-config/)" by [Muhammad Rehan Saeed](https://rehansaeed.com/)

Cookies
-------
It adds SameSite support (in LAX by default)
```xml
<configuration>
  <system.webServer>
    <rewrite>
      <outboundRules>
        <rule name="Add SameSite" preCondition="No SameSite">
          <match serverVariable="RESPONSE_Set_Cookie" pattern=".*" negate="false" />
          <action type="Rewrite" value="{R:0}; SameSite=lax" />
          <conditions>
          </conditions>
        </rule>
        <preConditions>
          <preCondition name="No SameSite">
            <add input="{RESPONSE_Set_Cookie}" pattern="." />
            <add input="{RESPONSE_Set_Cookie}" pattern="OpenIdConnect\.nonce\..+" negate="true" />
            <add input="{RESPONSE_Set_Cookie}" pattern="; SameSite=lax" negate="true" />
          </preCondition>
        </preConditions>
      </outboundRules>
    </rewrite>
  </system.webServer>
</configuration>
```
> Inspired by "[Adding SameSite Cookie Support In ASP.NET](https://kamranicus.com/posts/2017-02-23-samesite-cookies-in-asp-net)" by [Kamran Ayub](https://kamranicus.com/)
and "[Cross-Site Request Forgery is dead!](https://scotthelme.co.uk/csrf-is-dead/)" by [Scott Helme](https://scotthelme.co.uk/)

UMBRACO
-------
If you use this package with Umbraco you may add this section to your web.config:
```xml
<configuration>
  <location path="umbraco">
    <acme.web.security.headers xmlns="Acme.Web.Security.Headers" frameOptions="SameOrigin">
      <contentSecurityPolicy>
        <script self="true" unsafeInline="true" unsafeEval="true" />
        <style self="true" unsafeInline="true" />
        <img self="true">
          <add host="umbraco.tv" />
          <add host="dashboard.umbraco.org" />
        </img>
        <media>
          <add host="player.vimeo.com" />
          <add host="*.vimeocdn.com" />
        </media>
        <frameAncestors self="true" />
      </contentSecurityPolicy>
    </acme.web.security.headers>
  </location> 
</configuration>
```

Roadmap & Ideas
---------------
* Unit tests
* Dotnet Core support
* Change some specific cookie names related to ASP.NET like _ASP.NET_SessionId_, _Anti-Forgery Tokens_ 

Documentation
-------------

To avoid reinventing the wheel and making mistakes, all header documentation is extracted from [The MDN Web Docs](https://developer.mozilla.org/):
>"[X-Content-Type-Options](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options)" by [DBaron](https://developer.mozilla.org/en-US/profiles/DBaron), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [WhatIsHeDoing](https://developer.mozilla.org/en-US/profiles/WhatIsHeDoing), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).

>"[Referrer-Policy](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy)" by [Ginkoid](https://developer.mozilla.org/en-US/profiles/Ginkoid), [dhausknecht](https://developer.mozilla.org/en-US/profiles/dhausknecht), [twm](https://developer.mozilla.org/en-US/profiles/twm), [ptamarit](https://developer.mozilla.org/en-US/profiles/ptamarit), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [chrisdavidmills](https://developer.mozilla.org/en-US/profiles/chrisdavidmills), [lfaraone](https://developer.mozilla.org/en-US/profiles/lfaraone), [pox](https://developer.mozilla.org/en-US/profiles/pox) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).

>"[Strict-Transport-Security](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Strict-Transport-Security)" by [simplenotezy](https://developer.mozilla.org/en-US/profiles/simplenotezy), [plygrnd](https://developer.mozilla.org/en-US/profiles/plygrnd), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [hmft](https://developer.mozilla.org/en-US/profiles/hmft), [gregoryh](https://developer.mozilla.org/en-US/profiles/gregoryh), [AxelF](https://developer.mozilla.org/en-US/profiles/AxelF), [koluke](https://developer.mozilla.org/en-US/profiles/koluke), [jwhitlock](https://developer.mozilla.org/en-US/profiles/jwhitlock), [Sebastianz](https://developer.mozilla.org/en-US/profiles/Sebastianz), [Tom_D](https://developer.mozilla.org/en-US/profiles/Tom_D), [pijaykrause](https://developer.mozilla.org/en-US/profiles/pijaykrause), [jswisher](https://developer.mozilla.org/en-US/profiles/jswisher), [fowl2](https://developer.mozilla.org/en-US/profiles/fowl2), [coffeina](https://developer.mozilla.org/en-US/profiles/coffeina), [wbamberg](https://developer.mozilla.org/en-US/profiles/wbamberg), [dbaxa](https://developer.mozilla.org/en-US/profiles/dbaxa), [Annevk](https://developer.mozilla.org/en-US/profiles/Annevk), [konklone](https://developer.mozilla.org/en-US/profiles/konklone), [teoli](https://developer.mozilla.org/en-US/profiles/teoli), [AD7six](https://developer.mozilla.org/en-US/profiles/AD7six), [Sheppy](https://developer.mozilla.org/en-US/profiles/Sheppy), [wayno](https://developer.mozilla.org/en-US/profiles/wayno), [mkato](https://developer.mozilla.org/en-US/profiles/mkato), [tregagnon](https://developer.mozilla.org/en-US/profiles/tregagnon), [st3fan](https://developer.mozilla.org/en-US/profiles/st3fan), [yyss](https://developer.mozilla.org/en-US/profiles/yyss), [paul.irish](https://developer.mozilla.org/en-US/profiles/paul.irish), [mcoates@mozilla.com](https://developer.mozilla.org/en-US/profiles/mcoates@mozilla.com), [CesarB](https://developer.mozilla.org/en-US/profiles/CesarB), [sidstamm](https://developer.mozilla.org/en-US/profiles/sidstamm) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).

>"[X-XSS-Protection](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection)" by [ccsplit](https://developer.mozilla.org/en-US/profiles/ccsplit), [Slaweally](https://developer.mozilla.org/en-US/profiles/Slaweally), [arthurwhite](https://developer.mozilla.org/en-US/profiles/arthurwhite), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).

>"[X-Frame-Options](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options)" by [jpmedley](https://developer.mozilla.org/en-US/profiles/jpmedley), [Jas0n99](https://developer.mozilla.org/en-US/profiles/Jas0n99), [shellac_](https://developer.mozilla.org/en-US/profiles/shellac_), [comfytoday](https://developer.mozilla.org/en-US/profiles/comfytoday), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [SphinxKnight](https://developer.mozilla.org/en-US/profiles/SphinxKnight), [Benedito1](https://developer.mozilla.org/en-US/profiles/Benedito1), [kishore333](https://developer.mozilla.org/en-US/profiles/kishore333), [teoli](https://developer.mozilla.org/en-US/profiles/teoli), [JesseNaranjo](https://developer.mozilla.org/en-US/profiles/JesseNaranjo), [Sebastianz](https://developer.mozilla.org/en-US/profiles/Sebastianz), [freddyb](https://developer.mozilla.org/en-US/profiles/freddyb), [Sheppy](https://developer.mozilla.org/en-US/profiles/Sheppy), [foxbrush](https://developer.mozilla.org/en-US/profiles/foxbrush), [Hsvnsson](https://developer.mozilla.org/en-US/profiles/Hsvnsson), [Starefossen](https://developer.mozilla.org/en-US/profiles/Starefossen), [Ellani cola](https://developer.mozilla.org/en-US/profiles/Ellanicola), [Daniel Veditz](https://developer.mozilla.org/en-US/profiles/Daniel%20Veditz), [anthonyryan1](https://developer.mozilla.org/en-US/profiles/anthonyryan1), [estelle](https://developer.mozilla.org/en-US/profiles/estelle), [caioproiete](https://developer.mozilla.org/en-US/profiles/caioproiete), [kscarfone](https://developer.mozilla.org/en-US/profiles/kscarfone), [rothshahar](https://developer.mozilla.org/en-US/profiles/rothshahar), [gasubic](https://developer.mozilla.org/en-US/profiles/gasubic), [maybe](https://developer.mozilla.org/en-US/profiles/maybe), [rockad](https://developer.mozilla.org/en-US/profiles/rockad), [mnoorenberghe](https://developer.mozilla.org/en-US/profiles/mnoorenberghe), [localhorst](https://developer.mozilla.org/en-US/profiles/localhorst), [yyss](https://developer.mozilla.org/en-US/profiles/yyss), [Psz](https://developer.mozilla.org/en-US/profiles/Psz) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).

>"[Content Security Policy (CSP)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP)" by [vipsh18](https://developer.mozilla.org/en-US/profiles/vipsh18), [PeterDavidCarter](https://developer.mozilla.org/en-US/profiles/PeterDavidCarter), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [David-5-1](https://developer.mozilla.org/en-US/profiles/David-5-1), [allstars.chh](https://developer.mozilla.org/en-US/profiles/allstars.chh), [jpmedley](https://developer.mozilla.org/en-US/profiles/jpmedley), [coolaj86](https://developer.mozilla.org/en-US/profiles/coolaj86), [hooch](https://developer.mozilla.org/en-US/profiles/hooch), [partizanos](https://developer.mozilla.org/en-US/profiles/partizanos), [teoli](https://developer.mozilla.org/en-US/profiles/teoli), [chrisdavidmills](https://developer.mozilla.org/en-US/profiles/chrisdavidmills), [blackoutjack](https://developer.mozilla.org/en-US/profiles/blackoutjack), [Sheppy](https://developer.mozilla.org/en-US/profiles/Sheppy), [davidbgk](https://developer.mozilla.org/en-US/profiles/davidbgk), [imelven](https://developer.mozilla.org/en-US/profiles/imelven), [dregad](https://developer.mozilla.org/en-US/profiles/dregad), [abarth](https://developer.mozilla.org/en-US/profiles/abarth), [devdatta](https://developer.mozilla.org/en-US/profiles/devdatta), [mathjazz](https://developer.mozilla.org/en-US/profiles/mathjazz), [Marsf](https://developer.mozilla.org/en-US/profiles/Marsf), [mnoorenberghe](https://developer.mozilla.org/en-US/profiles/mnoorenberghe), [jswisher](https://developer.mozilla.org/en-US/profiles/jswisher), [bsterne](https://developer.mozilla.org/en-US/profiles/bsterne), [fryn](https://developer.mozilla.org/en-US/profiles/fryn) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: child-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/child-src)" by [sideshowbarker](https://developer.mozilla.org/en-US/profiles/sideshowbarker), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: connect-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/connect-src)" by [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: default-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/default-src)" by [jpmedley](https://developer.mozilla.org/en-US/profiles/jpmedley), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [stuajc](https://developer.mozilla.org/en-US/profiles/stuajc), [teoli](https://developer.mozilla.org/en-US/profiles/teoli), [sergiferran](https://developer.mozilla.org/en-US/profiles/sergiferran) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: font-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/font-src)" by [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: form-action](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/form-action)" by [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: frame-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/frame-src)" by [twm](https://developer.mozilla.org/en-US/profiles/twm), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: frame-ancestors](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/frame-ancestors)" by [JasonTarka](https://developer.mozilla.org/en-US/profiles/JasonTarka), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: img-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/img-src)" by [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: manifest-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/manifest-src)" by [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: media-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/media-src)" by [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: object-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/object-src)" by [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: script-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/script-src)" by [thinktt](https://developer.mozilla.org/en-US/profiles/thinktt), [vic511](https://developer.mozilla.org/en-US/profiles/vic511), [Braiam](https://developer.mozilla.org/en-US/profiles/Braiam), [jpmedley](https://developer.mozilla.org/en-US/profiles/jpmedley), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [DaleGardner](https://developer.mozilla.org/en-US/profiles/DaleGardner), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: style-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/style-src)" by [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: worker-src](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/worker-src)" by [yvanavermaet](https://developer.mozilla.org/en-US/profiles/yvanavermaet), [chrisdavidmills](https://developer.mozilla.org/en-US/profiles/chrisdavidmills), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [teoli](https://developer.mozilla.org/en-US/profiles/teoli) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).
"[CSP: require-sri-for](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/require-sri-for)" by [Sheppy](https://developer.mozilla.org/en-US/profiles/Sheppy), [fscholz](https://developer.mozilla.org/en-US/profiles/fscholz), [phillycheeze](https://developer.mozilla.org/en-US/profiles/phillycheeze), [freddyb](https://developer.mozilla.org/en-US/profiles/freddyb) is licensed under [CC-BY-SA 2.5](http://creativecommons.org/licenses/by-sa/2.5/).

If I’ve forgot to mention you, send me pull request with the correct attribution and I will update it ASAP ;-)