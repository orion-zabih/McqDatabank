using System.Web.Mvc;
using MsAspMvc = System.Web.Mvc.Html;
using System.Web.Routing;
using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace KTS_Testing_System.Extensions
{
    public static class FormExtensions
    {
        public static FormDisposer BeginAngularForm(this HtmlHelper htmlHelper, FormMethod method,object htmlAttributes)
        {
            RouteValueDictionary routeValues = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var form = new TagBuilder("form");
            if (!routeValues.ContainsKey("name"))
                routeValues.Add("name", "form");
            
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(htmlAttributes))
            {
                // string_htmlAttributes += string.Format("{0}=\"{1}\" ", property.Name.Replace('_', '-'), property.GetValue(htmlAttributes));
                string key = property.Name;
                string value = property.GetValue(htmlAttributes).ToString();
                form.Attributes.Add(key, value);
            }

       



            htmlHelper.ViewData.Add("formName", routeValues["name"]);
            
            

            string htmlattr = htmlAttributes.ToString();
            htmlHelper.ViewContext.Writer
                       .Write(form.ToString(
                                            TagRenderMode.StartTag));
            return new  FormDisposer(htmlHelper.ViewContext);
        }

      
    }

    public class FormDisposer : IDisposable
    {
        private readonly TextWriter _writer;
        public FormDisposer(ViewContext viewContext)
        {
            _writer = viewContext.Writer;
        }

        public void Dispose()
        {
            this._writer.Write("</form>");
        }
    }

}