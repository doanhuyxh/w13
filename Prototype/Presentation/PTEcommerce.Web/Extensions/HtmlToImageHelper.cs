using HtmlToImage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PTEcommerce.Web.Extensions
{
    public class HtmlToImageHelper
    {
        public static void ExportImageHagoOrder(string orderId, string nick, string hagoId, string avatar, int diamond, string time, string content)
        {
            string path = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Images/Order/"), orderId + ".jpg");
            var converter = new HtmlConverter();
            string html = File.ReadAllText(HttpContext.Current.Server.MapPath("/MailTemplates/template_orderhago.html"));
            html = html.Replace("{Time}", time);
            html = html.Replace("{Avatar}", avatar);
            html = html.Replace("{Nick}", nick);
            html = html.Replace("{Diamond}", diamond.ToString());
            html = html.Replace("{HagoID}", hagoId);
            html = html.Replace("{Content}", content);
            var bytes = converter.FromHtmlString(html, 700);
            File.WriteAllBytes(path, bytes);
        }
    }
}