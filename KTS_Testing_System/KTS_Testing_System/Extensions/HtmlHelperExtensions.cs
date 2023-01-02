using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace KTS_Testing_System.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DisableIf(this MvcHtmlString htmlString, Func<bool> expression)
        {
            if (expression.Invoke())
            {
                var html = htmlString.ToString();
                const string disabled = "\"readonly\"";
                html = html.Insert(html.IndexOf(">",
                  StringComparison.Ordinal), " readonly = " + disabled);
                return new MvcHtmlString(html);
            }
            return htmlString;
        }

        public static MvcHtmlString EditorForMany<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, IEnumerable<TValue>>> expression, string htmlFieldName = null) where TModel : class
        {
            var items = expression.Compile()(html.ViewData.Model);
            string style = "";
            var sb = new StringBuilder();

            if (String.IsNullOrEmpty(htmlFieldName))
            {
                var prefix = html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;

                htmlFieldName = (prefix.Length > 0 ? (prefix + ".") : String.Empty) + ExpressionHelper.GetExpressionText(expression);
            }
            //if (!htmlFieldName.Contains("focalPersonCollection_DIV"))
            //{
            //    return new MvcHtmlString(string.Empty);
            //}
            //else
            {
                foreach (var item in items)
                {
                    var dummy = new { Item = item };
                    var guid = Guid.NewGuid().ToString();
                    var memberExp = Expression.MakeMemberAccess(Expression.Constant(dummy), dummy.GetType().GetProperty("Item"));
                    var singleItemExp = Expression.Lambda<Func<TModel, TValue>>(memberExp, expression.Parameters);

                    if (htmlFieldName.Trim().Contains("_SNG"))
                    {
                        sb.Append(String.Format(@"<input type=""hidden"" name=""{0}.Index"" value=""{1}"" />", htmlFieldName, guid));
                        sb.Append(html.EditorFor(singleItemExp, null, String.Format("{0}[{1}]", htmlFieldName, guid)));
                    }

                    else if (htmlFieldName.Trim().Contains("_DIV"))
                    {
                        sb.Append(String.Format(@"<div id={0} class='row panel panel-success'>", guid));
                        sb.Append(html.EditorFor(singleItemExp, null, String.Format("{0}[{1}]", htmlFieldName, guid)));
                        sb.Append(String.Format(@"<input type=""hidden"" name=""{0}.Index"" value=""{1}"" />", htmlFieldName, guid));
                        sb.Append(String.Format(@"</div>", guid));
                    }
                    else if (htmlFieldName.Trim().Contains("_SUBDIV"))
                    {
                        sb.Append(html.EditorFor(singleItemExp, null, String.Format("{0}[{1}]", htmlFieldName, guid)));
                    }
                    else if (htmlFieldName.Trim().Contains("_TBL"))
                    {
                        sb.Append(String.Format(@"<tr id={0}>", guid));
                        sb.Append(String.Format(@"<input type=""hidden"" name=""{0}.Index"" value=""{1}"" />", htmlFieldName, guid));
                        sb.Append(html.EditorFor(singleItemExp, null, String.Format("{0}[{1}]", htmlFieldName, guid)));
                        //sb.Append(String.Format(@"<td><button type='button' class='btn btn-default' onclick=DeleteLocation('{0}')>Delete</button></td></tr>", guid));
                        //sb.Append(String.Format(@"</tr>", guid));
                    }
                    else
                    {
                        sb.Append(String.Format(@"<div id={0} class='row panel panel-success'>", guid));
                        sb.Append(html.EditorFor(singleItemExp, null, String.Format("{0}[{1}]", htmlFieldName, guid)));
                        sb.Append(String.Format(@"<input type=""hidden"" name=""{0}.Index"" value=""{1}"" />", htmlFieldName, guid));
                        sb.Append(String.Format(@"<div class='form-group col-md-2 display-table'><button type='button' class='btn btn-danger display-cell floatright' onclick=DeleteLocation('" + htmlFieldName + "','" + guid + "')>Delete</button></div></div>", guid));
                    }
                }
            }

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString HiddenCollection<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, IEnumerable<TValue>>> expression, string htmlFieldName = null) where TModel : class
        {
            var items = expression.Compile()(html.ViewData.Model);
            string style = "";
            var sb = new StringBuilder();

            if (String.IsNullOrEmpty(htmlFieldName))
            {
                var prefix = html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;

                htmlFieldName = (prefix.Length > 0 ? (prefix + ".") : String.Empty) + ExpressionHelper.GetExpressionText(expression);
            }

            foreach (var item in items)
            {
                var dummy = new { Item = item };
                var guid = Guid.NewGuid().ToString();

                var memberExp = Expression.MakeMemberAccess(Expression.Constant(dummy), dummy.GetType().GetProperty("Item"));
                var singleItemExp = Expression.Lambda<Func<TModel, TValue>>(memberExp, expression.Parameters);


                sb.Append(String.Format(@"<tr id={0} >", guid));
                sb.Append(String.Format(@"<input type=""hidden"" name=""{0}.Index"" value=""{1}"" />", htmlFieldName, guid));
                sb.Append(html.EditorFor(singleItemExp, null, String.Format("{0}[{1}]", htmlFieldName, guid)));
                sb.Append(String.Format(@"</tr>", guid));

            }

            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        /// Return Partial View.
        /// The element naming convention is maintained in the partial view by setting the prefix name from the expression.
        /// The name of the view (by default) is the class name of the Property or a UIHint("partial name").
        /// @Html.PartialFor(m => m.Address)  - partial view name is the class name of the Address property.
        /// </summary>
        /// <param name="expression">Model expression for the prefix name (m => m.Address)</param>
        /// <returns>Partial View as Mvc string</returns>
        public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            return html.PartialFor(expression, null);
        }

        /// <summary>
        /// Return Partial View.
        /// The element naming convention is maintained in the partial view by setting the prefix name from the expression.
        /// </summary>
        /// <param name="partialName">Partial View Name</param>
        /// <param name="expression">Model expression for the prefix name (m => m.Group[2])</param>
        /// <returns>Partial View as Mvc string</returns>
        public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string partialName
            )
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            string modelName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            object model = metaData.Model;


            if (partialName == null)
            {
                partialName = metaData.TemplateHint == null
                    ? typeof(TProperty).Name    // Class name
                    : metaData.TemplateHint;    // UIHint("template name")
            }

            // Use a ViewData copy with a new TemplateInfo with the prefix set
            ViewDataDictionary viewData = new ViewDataDictionary(html.ViewData)
            {
                TemplateInfo = new TemplateInfo { HtmlFieldPrefix = modelName }
            };

            // Call standard MVC Partial
            return html.Partial(partialName, model, viewData);
        }

      
        }
    }