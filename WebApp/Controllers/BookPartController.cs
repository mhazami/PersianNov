using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersianNov.DataStructure;
using PersianNov.Services;
using Radyn.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using static PersianNov.DataStructure.Tools.Enums;

namespace Author.Controllers
{
    [Authorize(Roles = Constant.Author)]
    public class BookPartController : Controller
    {

        public async Task<IActionResult> Index(Guid bookId)
        {
            var BookParts = await PersianNovComponent.Instance.BookPartFacade.WhereAsync(x => x.BookId == bookId);
            return View(BookParts);
        }

        public IActionResult Create()
        {
            ViewBag.Message = "";
            ViewBag.Janre = new SelectList(EnumUtils.ConvertEnumToIEnumerable<Janre>(), "Key", "Value");
            return View(new BookPart());
        }

        [HttpPost]
        public IActionResult Create(BookPart BookPart, IFormFile image, IFormFile pdf)
        {
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                BookPart.AuthorId = userId.ToInt();
                BookPart.PublishDate = DateTime.Now.ShamsiDate();
                if (!PersianNovComponent.Instance.BookPartFacade.Insert(BookPart, image, pdf))
                    throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(BookPart);
            }
        }
    }
}