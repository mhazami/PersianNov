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
using WebApp.Models;
using static PersianNov.DataStructure.Tools.Enums;

namespace Author.Controllers
{
    [Authorize(Roles = Constant.Author)]
    public class BookController : Controller
    {

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var books = await PersianNovComponent.Instance.BookFacade.WhereAsync(x => x.AuthorId == user.ToGuid());
            return View(books);
        }

        public IActionResult Create()
        {
            ViewBag.Message = "";
            ViewBag.Janre = new SelectList(EnumUtils.ConvertEnumToIEnumerable<Janre>(), "Key", "Value");
            return View(new Book());
        }


        [HttpPost]
        public IActionResult Create(Book book, IFormFile image, IFormFile pdf)
        {
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                book.AuthorId = userId.ToGuid();
                book.PublishDate = DateTime.Now.ShamsiDate();
                if (!PersianNovComponent.Instance.BookFacade.Insert(book, image, pdf))
                    throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(book);
            }
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var authorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (PersianNovComponent.Instance.AuthorFacade.CheckBookOwner(authorId.ToGuid(), id))
            {
                var book = await PersianNovComponent.Instance.BookFacade.GetAsync(id);
                return View(book);
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }
           
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit()
        //{

        //}

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}