#pragma checksum "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "251510baae0f0ad11eeb3f47d8da6b3ee07b9bf1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(WebApp.Pages.Rally.Pages_Rally_OutOfStockReport), @"mvc.1.0.razor-page", @"/Pages/Rally/OutOfStockReport.cshtml")]
namespace WebApp.Pages.Rally
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
#line 1 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\_ViewImports.cshtml"
using WebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
using Domain;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"251510baae0f0ad11eeb3f47d8da6b3ee07b9bf1", @"/Pages/Rally/OutOfStockReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab56b927680bad6a0ba24d2e6e24966a4e74b8b3", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Rally_OutOfStockReport : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page-handler", "GoToRally", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
  
    ViewData["Title"] = $"Out of Stock Report";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<table class=""table"">
    <thead>
        <tr>
            <th>Category</th>
            <th>Name</th>
            <th>Location</th>
            <th>Currently In Stock</th>
            <th>Optimal Stock Stock</th>
            <th class =""text-danger"">Missing Stock Stock</th>
            <th>Restock Price</th>

        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 23 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
 foreach (var item in Model.Items) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 25 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
           Write(item.Category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 26 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
           Write(item.ItemName.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 27 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
           Write(item.Location.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 28 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
           Write(item.CurrentQuantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 29 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
           Write(item.OptimalQuantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td class =\"text-danger\">");
#nullable restore
#line 30 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
                                 Write(item.OptimalQuantity - item.CurrentQuantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 31 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
            Write((item.OptimalQuantity - item.CurrentQuantity) * item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" €</td>\r\n        </tr>\r\n");
#nullable restore
#line 33 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "251510baae0f0ad11eeb3f47d8da6b3ee07b9bf16788", async() => {
                WriteLiteral("Back to Rally");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.PageHandler = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-rallyId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 38 "C:\Users\Karmo\Desktop\C#\C# gameDev\icd0008-2020f\WebAppExam01\WebApp\Pages\Rally\OutOfStockReport.cshtml"
                                           WriteLiteral(Model.RallyId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["rallyId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-rallyId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["rallyId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApp.Pages.Rally.OutOfStockReport> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApp.Pages.Rally.OutOfStockReport> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApp.Pages.Rally.OutOfStockReport>)PageContext?.ViewData;
        public WebApp.Pages.Rally.OutOfStockReport Model => ViewData.Model;
    }
}
#pragma warning restore 1591
