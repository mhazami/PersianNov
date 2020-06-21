using CoreHtmlToImage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace PersianNov.DataStructure.Tools
{
    public static class Utils
    {
        public static string SlugMaker(this string slug)
        {
            return slug.Trim().Replace(' ', '-');
        }

        public static string RecoverSlug(this string slug)
        {
            return slug.Trim().Replace('-', ' ');
        }

        public static byte[] ConvertTextToHtml(this string text)
        {
            var converter = new HtmlConverter();
            var html = $"<!DOCTYPE html><html lang='fa'><head><meta charset='utf-8' /><style>p,span{{font-size:26px !important;}}</style>  </head><body style='font-size:20px;font-family:tanha;'><div style='font-size:150px;position:fixed;top:10%;right:50px;opacity:0.5;z-index:999;color:#ddd;'>رمان فارسی</div><div style='font-size:80px;position:fixed;top:50%;right:50px;opacity:0.5;z-index:999;color:#ddd;'>www.persiannovel.com</div><div style='font-size:150px;position:fixed;top:80%;right:50px;opacity:0.5;z-index:999;color:#ddd;'>رمان فارسی</div>{text}</body></html>";
            return converter.FromHtmlString(html);
        }



    }
    
}
