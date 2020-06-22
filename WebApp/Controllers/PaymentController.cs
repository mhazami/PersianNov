using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersianNov.DataStructure;
using PersianNov.DataStructure.Tools;
using PersianNov.Services;
using Radyn.Framework;
using Radyn.Utility;
using WebApp.Models;
using Zarinpal;

namespace WebApp.Controllers
{
    public class PaymentController : Controller
    {
        private IConfiguration config;
        public PaymentController(IConfiguration configuration)
        {
            config = configuration;
        }
        [Authorize(Roles = Constant.Author)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Constant.Customer)]
        [Route("/رمان-فارسی/{title}/{id}")]
        [ActionName("BookPaymentInfo")]
        public async Task<IActionResult> BookPaymentInfo(Guid id, string title)
        {
            var customerId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var model = new CustomerBook()
            {
                BookId = id,
                Book = await PersianNovComponent.Instance.BookFacade.GetAsync(id),
                CustomerId = customerId.ToGuid(),
                Customer = await PersianNovComponent.Instance.CustomerFacade.GetAsync(customerId.ToGuid())
            };
            return View(model);
        }


        [Authorize(Roles = Constant.Customer)]
        [ActionName("BookPaymentInfo"), HttpPost]
        public async Task<IActionResult> BookPaymentInfo(CustomerBook customerBook)
        {
            try
            {
                var book = PersianNovComponent.Instance.BookFacade.Get(customerBook.BookId);
                var customer = PersianNovComponent.Instance.CustomerFacade.Get(customerBook.CustomerId);
                if (book != null && book.Price != 0)
                {
                    var sum = book.Discount != 0 ? book.Price - (book.Price * book.Discount / 100) : book.Price;
                    var payment = new Zarinpal.Payment("b64b52e2-ac94-11ea-8aff-000c295eb8fc", System.Convert.ToInt32(sum));
                    var result = payment.PaymentRequest("پرداخت کتاب " + book.Name,
                        $"{config.GetSection("OrginUrl").Value}/فاکتور/{customerBook.BookId}",
                                                customer.Email);
                    if (result.Result.Status == 100)
                    {
                        return Redirect("https://zarinpal.com/pg/StartPay/" + result.Result.Authority);
                    }
                    else
                    {
                        throw new KnownException("خطایی در روند پرداخت رخ داده است لطفا با واحد پشتیبانی تماس بگیرید");
                    }
                }
                customerBook.Book = book;
                customerBook.Customer = customer;
                return View(customerBook);

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(customerBook);
            }
        }

        [Route("/فاکتور/{id}")]
        [Authorize(Roles = Constant.Customer)]
        public IActionResult PaymentResult(Guid id)
        {
            var customerId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var book = PersianNovComponent.Instance.BookFacade.Get(id);
                var sum = book.Discount != 0 ? book.Price - (book.Price * book.Discount / 100) : book.Price;

                var payment = new Zarinpal.Payment("b64b52e2-ac94-11ea-8aff-000c295eb8fc", System.Convert.ToInt32(sum));
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                }

            }
            ViewBag.code = 0;
            return View();
        }
    }
}