using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace KTS_Testing_System.Extensions
{
    public static class LabelExtensions
    {
        public static MvcHtmlString LabelForWithSpan<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes,string InnerHTML)
        {
            return LabelForWithSpan(html, expression, new RouteValueDictionary(htmlAttributes),InnerHTML);
        }
        public static MvcHtmlString LabelForWithSpan<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, string InnerHTML)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.InnerHtml = InnerHTML;

            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            //TagBuilder span = new TagBuilder("span");
            //span.SetInnerText(labelText);

            //// assign <span> to <label> inner html
            //tag.InnerHtml = span.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}
    