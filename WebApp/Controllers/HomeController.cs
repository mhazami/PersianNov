﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersianNov.Services;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
    
        public async Task<IActionResult> Index()
        {
            var author = User.Identity;
            var books = await PersianNovComponent.Instance.BookFacade.GetAllAsync();
            return View(books);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
