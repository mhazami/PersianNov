using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersianNov.DataStructure;
using PersianNov.Services;
using Radyn.Utility;
using static PersianNov.DataStructure.Tools.Enums;

namespace Author.Controllers
{
    [Authorize(Roles = "Author")]
    public class BookController : Controller
    {

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var books = await PersianNovComponent.Instance.BookFacade.WhereAsync(x => x.AuthorId == user.ToInt());
            return View(books);
        }

        public IActionResult Create()
        {
            ViewBag.Message = "";
            ViewBag.Janre = new SelectList(EnumUtils.ConvertEnumToIEnumerable<Janre>(), "Key", "Value");
            return View(new Book());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book, IFormFile image, IFormFile pdf)
        {
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                book.AuthorId = userId.ToInt();
                book.PublishDate = DateTime.Now.ShamsiDate();
                if (!await PersianNovComponent.Instance.BookFacade.InsertAsync(book, image, pdf))
                    throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(book);
            }
        }
    }
}