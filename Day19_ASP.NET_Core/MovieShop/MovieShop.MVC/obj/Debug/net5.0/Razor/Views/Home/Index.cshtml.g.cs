#pragma checksum "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b5426060b5d4642a2d0ac338b3322a3ab3aea2ac"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\_ViewImports.cshtml"
using MovieShop.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\_ViewImports.cshtml"
using MovieShop.MVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5426060b5d4642a2d0ac338b3322a3ab3aea2ac", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"53ed27a90769d57c4cf1e99ddf07e56b08d479e3", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<ApplicationCore.Models.Response.MovieCardResponseModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 4 "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>\r\n    ");
#nullable restore
#line 9 "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\Home\Index.cshtml"
Write(ViewBag.PageTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</h2>\r\n\r\n<h3>\r\n    Total Movies : ");
#nullable restore
#line 13 "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\Home\Index.cshtml"
              Write(ViewBag.MoviesCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</h3>\r\n\r\n<h3>\r\n    ");
#nullable restore
#line 17 "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\Home\Index.cshtml"
Write(ViewData["MyCustomData"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</h3>\r\n\r\n");
#nullable restore
#line 20 "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\Home\Index.cshtml"
 foreach (var movie in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        ");
#nullable restore
#line 23 "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\Home\Index.cshtml"
   Write(movie.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 25 "D:\Temp\Antra\Day19_ASP.NET_Core\MovieShop\MovieShop.MVC\Views\Home\Index.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<ApplicationCore.Models.Response.MovieCardResponseModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
