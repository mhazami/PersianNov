using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersianNov.DataStructure;
using PersianNov.Services;

namespace WebApp.Controllers
{
    public class FileController : Controller
    {
        public async Task<IActionResult> ShowImage(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                File model = await PersianNovComponent.Instance.FileFacade.GetAsync(id);
                return File(model.Content, model.ContentType);
            }
            return new EmptyResult();
        }
    }
}