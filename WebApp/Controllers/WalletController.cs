using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersianNov.Services;
using Radyn.Utility;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class WalletController : Controller
    {
      
        [Authorize(Roles = Constant.Author)]
        public IActionResult Wallet()
        {
            var authorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var cash = PersianNovComponent.Instance.WalletFacade.GetAuthorAmount(authorId.ToGuid());
            ViewBag.Cash = $"{cash.ToString("N0")} تومان";
            var list = PersianNovComponent.Instance.WalletFacade.Where(x => x.AuthorId == authorId.ToGuid());
            return View(list);
        }

        public IActionResult Details(Guid id)
        {
            var wallet = PersianNovComponent.Instance.WalletFacade.Get(id);
            return View(wallet);
        }



    }
}