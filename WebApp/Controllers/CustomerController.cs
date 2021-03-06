﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PersianNov.DataStructure;
using PersianNov.Services;

namespace WebApp.Controllers
{
    public class CustomerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var list = await PersianNovComponent.Instance.CustomerFacade.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.ReturnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string ReturnUrl)
        {
            try
            {
                var customer = await PersianNovComponent.Instance.CustomerFacade.Login(username, password);
                if (customer != null)
                {
                    this.SetCookie(customer);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);
                    return Redirect("/");
                }
                else
                {
                    ViewBag.Message = "نام کاربری یا رمز عبور اشتباه میباشد";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [Route("/ثبت-نام-مخاطب")]
        [ActionName("Register"), HttpGet]
        public IActionResult Register()
        {
            ViewBag.Message = "";
            return View(new Customer());
        }


        [ActionName("Register"), HttpPost]
        public async Task<IActionResult> Register(Customer customer)
        {
            try
            {
                if (await PersianNovComponent.Instance.CustomerFacade.InsertAsync(customer))
                {
                    this.SetCookie(customer);
                    return Redirect("/");

                }
                else
                {
                    ViewBag.Message = "خطایی در زمان ثبت نام رخ داده است لطفا بعدا مجدد تلاش کنید"; ;
                    return View(customer);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(customer);
            }
        }


        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [Route("/فاکتور/{id}/{customerId}")]
        public IActionResult PaymentResult(Guid id, Guid customerId)
        {
            var book = PersianNovComponent.Instance.BookFacade.Get(id);
            var customer = PersianNovComponent.Instance.CustomerFacade.Get(customerId);

            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var sum = book.Discount > 0 ? book.Price - (book.Price * book.Discount / 100) : book.Price;
                var payment = new Zarinpal.Payment("b64b52e2-ac94-11ea-8aff-000c295eb8fc", System.Convert.ToInt32(sum));
                var res = payment.Verification(authority).Result;
                ViewBag.code = res.RefId;
                ViewBag.Status = res.Status;                
                if (res.Status == 100)
                {
                    
                    if (PersianNovComponent.Instance.PaymentFacade.InsertPayment(id, customerId, res.RefId))
                    {

                        SetCookie(customer);
                        return View(book);

                    }
                }
                              
            }
            SetCookie(customer);
            return View(book);
        }

        private async void SetCookie(Customer customer)
        {
            var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, $"{customer.FirstName} {customer.LastName}"),
                     new Claim(ClaimTypes.Email,customer.Email),
                     new Claim("Id",customer.Id.ToString()),
                     new Claim(ClaimTypes.Role, "Customer"),
                };


            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}