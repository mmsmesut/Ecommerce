#pragma checksum "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c6e72aed69a1f1b711bb2f7638483311ffa6679"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Order), @"mvc.1.0.view", @"/Views/Home/Order.cshtml")]
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
#line 1 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\_ViewImports.cshtml"
using Ecommerce.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\_ViewImports.cshtml"
using Ecommerce.UI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c6e72aed69a1f1b711bb2f7638483311ffa6679", @"/Views/Home/Order.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"90230061d40639ead6b09e50b4378d2d6de46d1d", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Order : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Ecommerce.Entity.Order>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml"
  
    ViewData["Title"] = "Order";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Sipraişler</h1>\r\n\r\n");
            WriteLiteral("\r\n<div class=\"row\">\r\n");
#nullable restore
#line 11 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml"
     if (Model.Any())
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml"
         foreach (var order in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-xs-12 col-sm-6 col-md-4\"> \r\n                <div class=\"product-grids product-item\">\r\n                    <div class=\"col-md-12\">Sipraiş Refrans No : ");
#nullable restore
#line 17 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml"
                                                           Write(order.UId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    <div class=\"col-md-12\">Sipraiş  No : ");
#nullable restore
#line 18 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml"
                                                    Write(order.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n                    <h5 style=\"margin-left:100px;\"></h5>\r\n                    <h5 style=\"margin-left:100px;\">\r\n                        Sipraiş  Adet :\r\n                        ");
#nullable restore
#line 23 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml"
                   Write(order.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </h5>\r\n                    <h5 style=\"margin-left:100px;\"></h5>\r\n                </div> \r\n            </div>\r\n");
#nullable restore
#line 28 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml"
         


    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"col-12\">\r\n            <div class=\"card\">\r\n                <div class=\"card-body\">\r\n                    You have not created or published any Order entries.\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 41 "D:\Special\Ecommerce\Ecommerce\Ecommerce.UI\Views\Home\Order.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Ecommerce.Entity.Order>> Html { get; private set; }
    }
}
#pragma warning restore 1591
