using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.ComponentModel;

namespace PTEcommerce.Web
{
    public class PageCtrl
    {
        public string Title { get; set; }
        public string PageNum { get; set; }
        public bool CurrentPage { get; set; }
    }
    public static class PagingHelper
    {
        public static MvcHtmlString PagingController(this HtmlHelper helper, int totalItems, int pageIndex, int pagesize)
        {
            string strPaging = "";
            var url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            NameValueCollection queryString = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());
            queryString.Remove("p");
            queryString.Remove("from");
            var sb = new StringBuilder();
            var strFormat = sb.Append(url).Append("?p={0}").Append((string.IsNullOrEmpty(queryString.ToString()) ? "" : ("&" + queryString.ToString()))).ToString();

            if (totalItems > pagesize)
            {
                int currentPage = pageIndex == 0 ? 1 : pageIndex;
                List<PageCtrl> pages = new List<PageCtrl>();
                int totalPages = (int)Math.Ceiling(((decimal)totalItems / pagesize));
                int startIndex = 0;
                int endIndex = totalPages;

                if (totalPages > 10)
                {
                    startIndex = currentPage - 5;
                    endIndex = currentPage + 5;
                    if (startIndex < 0)
                    {
                        startIndex = 0;
                        endIndex = startIndex + 10;
                    }
                    if (endIndex > totalPages)
                    {
                        endIndex = totalPages;
                        startIndex = totalPages - 10;
                    }
                }

                if (currentPage == 1)
                    pages.Add(new PageCtrl { Title = "|&lt;", PageNum = (currentPage).ToString(), CurrentPage = false });
                else
                    pages.Add(new PageCtrl { Title = "|&lt;", PageNum = "0", CurrentPage = false });
                for (int i = startIndex; i < endIndex; i++)
                {
                    PageCtrl page = new PageCtrl { Title = (i + 1).ToString(), PageNum = (i + 1).ToString(), CurrentPage = (i + 1) == (currentPage) };
                    pages.Add(page);
                }
                if (currentPage == totalPages)
                    pages.Add(new PageCtrl { Title = "&gt;|", PageNum = (currentPage).ToString(), CurrentPage = false });
                else
                    pages.Add(new PageCtrl { Title = "&gt;|", PageNum = (totalPages).ToString(), CurrentPage = false });

                string _url = "";
                var cs = "";
                var pd = string.Empty;
                foreach (PageCtrl x in pages)
                {
                    _url = string.Format(strFormat, (int.Parse(x.PageNum)));
                    if ((x.Title == "&gt;|") && (pageIndex == int.Parse(x.PageNum)))
                    {
                        cs = "next disabled";
                        pd = "last";
                    }
                    else if ((x.Title == "|&lt;") && (pageIndex == 0))
                    {
                        cs = "previous disabled";
                        pd = "first";
                    }
                    else
                    {
                        if (x.Title == "&gt;|")
                        {
                            cs = "next";
                            pd = "last";
                        }
                        else if (x.Title == "|&lt;")
                        {
                            cs = "previous";
                            pd = "first";
                        }
                        else
                        {
                            cs = "";
                            pd = x.PageNum.ToString();
                        }
                    }
                    if(x.CurrentPage)
                    {
                        strPaging += "<li class=\"active\"><span>" + x.Title + "</span></li>";
                    }
                    else
                    {
                        strPaging += "<li><a href='" + (_url.Length > 1000 ? _url.Substring(0, 1000) : _url) + "'>" + x.Title + "</a></li><li>";
                    }
                    //strPaging += "<a class='" + cs + " " + (x.CurrentPage ? "active item" : "item") + "' href='" + (_url.Length > 1000 ? _url.Substring(0, 1000) : _url) + "'>" + x.Title + "</a>";
                }
                strPaging = "<ul class=\"pagination\">" + strPaging + "</ul>";
            }
            return MvcHtmlString.Create(strPaging);
        }
    }
}