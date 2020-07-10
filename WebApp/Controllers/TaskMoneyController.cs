using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersianNov.DataStructure;
using PersianNov.DataStructure.Tools;
using PersianNov.Services;
using PersianNov.Services.Tools;
using Radyn.Framework;
using Radyn.Utility;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TaskMoneyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Constant.Author)]
        public IActionResult WithdrawList()
        {
            var authorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var cash = PersianNovComponent.Instance.WalletFacade.GetAuthorAmount(authorId.ToGuid());
            ViewBag.Cash = $"{cash.ToString("N0")} تومان";
            var list = PersianNovComponent.Instance.TaskMoneyFacade.OrderByDescending(x => x.RegisterDate, x => x.AuthorId == authorId.ToGuid()).OrderByDescending(x => x.Number);
            return View(list);
        }

        [Authorize(Roles = Constant.Author)]
        public IActionResult RegisterWithdraw()
        {
            var authorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var cash = PersianNovComponent.Instance.WalletFacade.GetAuthorAmount(authorId.ToGuid());
            ViewBag.Cash = $"{cash.ToString("N0")} تومان";
            var task = new TaskMoney()
            {
                AuthorId = authorId.ToGuid(),
                Amount = 0
            };

            return View(task);
        }

        [HttpPost]
        public IActionResult RegisterWithdraw(TaskMoney taskMoney)
        {
            try
            {
                var authorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
                if (taskMoney.AuthorId == null || taskMoney.AuthorId == Guid.Empty)
                    taskMoney.AuthorId = authorId.ToGuid();
                if (!PersianNovComponent.Instance.TaskMoneyFacade.Insert(taskMoney))
                    throw new KnownException("مشکلی در ثبت درخواست وجود دارد لطفا با پشتیبان تماس حاصل فرمایید");
                return RedirectToAction("WithdrawList");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(taskMoney);
            }

        }
    }
}