#pragma checksum "D:\Projectos\Projectos\AuthServer\IdentityServer4\UserAgentClient\Views\Home\PrivacyOicdFlow.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1f849ee209558b41351302286a5174c99830f610"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_PrivacyOicdFlow), @"mvc.1.0.view", @"/Views/Home/PrivacyOicdFlow.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Projectos\Projectos\AuthServer\IdentityServer4\UserAgentClient\Views\_ViewImports.cshtml"
using UserAgentClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projectos\Projectos\AuthServer\IdentityServer4\UserAgentClient\Views\_ViewImports.cshtml"
using UserAgentClient.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1f849ee209558b41351302286a5174c99830f610", @"/Views/Home/PrivacyOicdFlow.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2baa99b0d884444b778277bbaeaa22763e4da279", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_PrivacyOicdFlow : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Projectos\Projectos\AuthServer\IdentityServer4\UserAgentClient\Views\Home\PrivacyOicdFlow.cshtml"
  
    ViewData["Title"] = "Privacy Oicd Flow";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 4 "D:\Projectos\Projectos\AuthServer\IdentityServer4\UserAgentClient\Views\Home\PrivacyOicdFlow.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>

<p>Use this page to detail your site's privacy policy.</p>

<button onclick=""signOut()"">signOut</button>


<script src=""https://cdnjs.cloudflare.com/ajax/libs/oidc-client/1.11.6-beta.1/oidc-client.min.js""></script>
<script>
    var userManager = new Oidc.UserManager({
        userStore: new Oidc.WebStorageStateStore({store: window.localStorage}),
        response_mode: ""query"",
    });

    userManager.signinCallback().then(res =>
        {
            console.log(""Privacy Oicd Flow: "", res);
            //window.location.href = ""/home/index"";
        });

</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
