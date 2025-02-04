using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace ECommerce.UI.TagHelpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }
        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }
        [HtmlAttributeName("sort-az")]
        public bool? AZ { get; set; }
        [HtmlAttributeName("sort-higherToLower")]
        public bool? HigherToLower { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            var sb = new StringBuilder();
            string sortAlphabetic = AZ == null ? "" : AZ == true ? "&az=true" : "&az=false";
            string sortPrice = HigherToLower == null ? "" : HigherToLower == true ? "&higherToLower=true" : "&higherToLower=false";
            if (PageCount > 1)
            {
                
                sb.AppendFormat("<ul class='pagination'>");
                sb.AppendFormat("<li class='{0}'><a class='page-link' href='/product/index?page={1}&category={2}{3}{4}'>Previous</a></li>", (CurrentPage == 1 ? "page-item d-none" : "page-item"), (CurrentPage - 1), CurrentCategory, sortAlphabetic, sortPrice);
                for (int i = 1; i <= PageCount; i++)
                {
                    sb.AppendFormat("<li class ='{0}'>", (i==CurrentPage) ? "page-item active" : "page-item");
                    sb.AppendFormat("<a class='page-link' href='/product/index?page={0}&category={1}{3}{4}'>{2}</a>", i, CurrentCategory, i, sortAlphabetic, sortPrice);
                    sb.AppendFormat("</li>");
                }
                sb.AppendFormat("<li class='{0}'><a class='page-link' href='/product/index?page={1}&category={2}{3}{4}'>Next</a></li>", (CurrentPage == PageCount ? "page-item d-none" : "page-item"), (CurrentPage + 1), CurrentCategory, sortAlphabetic, sortPrice);
                sb.AppendFormat("</ul>");
            }
            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
