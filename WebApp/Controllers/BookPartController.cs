using System;
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
    public class BookPartController : Controller
    {
        public async Task<IActionResult> Index(Guid bookId)
        {
            var BookParts = await PersianNovComponent.Instance.BookPartFacade.WhereAsync(x => x.BookId == bookId);
            ViewBag.Book = PersianNovComponent.Instance.BookFacade.Get(bookId);
            return View(BookParts);
        }

        public IActionResult Create(Guid bookId)
        {
            ViewBag.Message = "";
            ViewBag.Janre = new SelectList(EnumUtils.ConvertEnumToIEnumerable<Janre>(), "Key", "Value");
            return View(new BookPart { BookId = bookId });
        }

        [HttpPost]
        public IActionResult Create(BookPart BookPart, IFormFile image)
        {
            try
            {
                BookPart.PublishDate = DateTime.Now.ShamsiDate();
                if (!PersianNovComponent.Instance.BookPartFacade.Insert(BookPart, image))
                    throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                return RedirectToAction("Index", new { bookId = BookPart.BookId });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(BookPart);
            }
        }
        public IActionResult Edit(Guid id)
        {
            var bookPart = PersianNovComponent.Instance.BookPartFacade.Get(id);
            return View(bookPart);
        }

        [HttpPost]
        public IActionResult Edit(BookPart bookPart, IFormFile image)
        {
            try
            {
                if (!PersianNovComponent.Instance.BookPartFacade.Update(bookPart, image))
                    throw new Exception("خطایی در ویرایش اطلاعات کتاب رخ داده است");
                return RedirectToAction("Index", new { bookId = bookPart.BookId });

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.InnerException.Message;
                return View(bookPart);
            }
        }

        public IActionResult Delete(Guid id)
        {
            var bookPart = PersianNovComponent.Instance.BookPartFacade.Get(id);
            return View(bookPart);
        }

        [HttpPost]
        public IActionResult Delete(BookPart bookPart)
        {
            try
            {
                if (!PersianNovComponent.Instance.BookPartFacade.Delete(bookPart.Id))
                    throw new Exception("خطایی در حذف اطلاعات کتاب رخ داده است");
                return RedirectToAction("Index", new { bookId = bookPart.BookId });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.InnerException.Message;
                return View(bookPart);
            }
        }


        public IActionResult Details(Guid id)
        {
            var bookPart = PersianNovComponent.Instance.BookPartFacade.Get(id);
            return View(bookPart);
        }
    }
}