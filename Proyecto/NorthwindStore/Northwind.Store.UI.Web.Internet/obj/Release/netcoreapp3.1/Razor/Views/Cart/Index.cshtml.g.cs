#pragma checksum "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19fcfa5dafc2479d5c9f4b47fe961b07ce8d6ebb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cart_Index), @"mvc.1.0.view", @"/Views/Cart/Index.cshtml")]
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
#line 1 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\_ViewImports.cshtml"
using Northwind.Store.UI.Web.Internet;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\_ViewImports.cshtml"
using Northwind.Store.UI.Web.Internet.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\_ViewImports.cshtml"
using Northwind.Store.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\_ViewImports.cshtml"
using Northwind.Store.UI.Web.Internet.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19fcfa5dafc2479d5c9f4b47fe961b07ce8d6ebb", @"/Views/Cart/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"41329aa3d9e2a819fdb1bc58c3fbb1cc9a822247", @"/Views/_ViewImports.cshtml")]
    public class Views_Cart_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CartViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml"
 if (Model != null && Model.Items.Any())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h3>Detalle del carrito</h3>\r\n        <ul>\r\n");
#nullable restore
#line 9 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml"
             foreach (var p in Model.Items)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>\r\n                    ");
#nullable restore
#line 12 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml"
               Write(p.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 12 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml"
                                Write(p.UnitPrice ?? 0);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </li>\r\n");
#nullable restore
#line 14 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n");
            WriteLiteral("        <p>Cantidad <strong>");
#nullable restore
#line 33 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml"
                       Write(Model.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></p>\r\n        <p>Total <strong>");
#nullable restore
#line 34 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml"
                    Write(Model.Total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></p>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "19fcfa5dafc2479d5c9f4b47fe961b07ce8d6ebb6834", async() => {
                WriteLiteral("Comprar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 36 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml"

        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h3>Su carrito está vacío!</h3>\r\n");
#nullable restore
#line 41 "C:\Cenfotec\WebApplicationDevASPNetCore\Proyecto\NorthwindStore\Northwind.Store.UI.Web.Internet\Views\Cart\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "19fcfa5dafc2479d5c9f4b47fe961b07ce8d6ebb8847", async() => {
                WriteLiteral("Regresar al Listado");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CartViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
