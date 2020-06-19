using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreHtmlToImage;
using Microsoft.AspNetCore.Mvc;
using PersianNov.DataStructure;
using PersianNov.Services;

namespace WebApp.Controllers
{
    public class FileController : Controller
    {
        public async Task<IActionResult> ShowImage(Guid id)
        {

            string url = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(url))
            {

                if (id != null && id != Guid.Empty)
                {
                    PersianNov.DataStructure.File model = await PersianNovComponent.Instance.FileFacade.GetAsync(id);
                    return File(model.Content, model.ContentType);
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> PartFile(Guid id)
        {
            string url = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(url))
            {
                var part = await PersianNovComponent.Instance.BookPartFacade.GetAsync(id);
                var file = ConvertTextToImage(part.Text, "tanha", 10, 150, 250);
                return File(file, "image/png");
            }
            return NotFound();
        }

        public byte[] ConvertTextToImage(string txt, string fontname, int fontsize, int width, int Height)
        {
            var converter = new HtmlConverter();
            var html = $"<!DOCTYPE html><html lang='fa'><head><meta charset='utf-8' /><style>p,span{{font-size:26px !important;}}</style>  </head><body style='font-size:20px;font-family:tanha;'><div style='font-size:150px;position:fixed;top:10%;right:50px;opacity:0.5;z-index:999;color:#ddd;'>رمان فارسی</div><div style='font-size:80px;position:fixed;top:50%;right:50px;opacity:0.5;z-index:999;color:#ddd;'>www.persiannovel.com</div><div style='font-size:150px;position:fixed;top:80%;right:50px;opacity:0.5;z-index:999;color:#ddd;'>رمان فارسی</div>{txt}</body></html>";
            var res = converter.FromHtmlString(html);
            //Bitmap bmp = new Bitmap(width, Height);
            //using (Graphics graphics = Graphics.FromImage(bmp))
            //{
            //    Font font = new Font(fontname, fontsize);
            //    graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
            //    graphics.DrawString(txt, font, new SolidBrush(Color.FromArgb(51, 51, 51)), 0, 0);
            //    graphics.Flush();
            //    font.Dispose();
            //    graphics.Dispose();
            //}
            //Response.ContentType = "image/Jpeg";
            //var ms = new MemoryStream();
            //bmp.Save(ms, ImageFormat.Jpeg);
            //var res = ms.ToArray();
            return res;
        }


    }
}