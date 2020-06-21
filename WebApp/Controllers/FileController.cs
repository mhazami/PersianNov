using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersianNov.DataStructure;
using PersianNov.DataStructure.Tools;
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

    }
}