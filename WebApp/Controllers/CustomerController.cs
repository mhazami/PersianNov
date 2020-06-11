using System;
using System.Collections.Generic;
using System.Linq;
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

        [Route("/ورود-مخاطب")]
        [ActionName("Login"), HttpGet]
        public IActionResult Login()
        {
            ViewBag.Message = "";
            return View();
        }

        [ActionName("Login"), HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var customer = await PersianNovComponent.Instance.CustomerFacade.Login(username, password);
                if (customer != null)
                {
                    this.SetCookie(customer);
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

        private async void SetCookie(Customer customer)
        {
            var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, $"{customer.FirstName} {customer.LastName}"),
                     new Claim(ClaimTypes.Email,customer.Email),
                     new Claim("Id",customer.Id.ToString()),
                     new Claim(ClaimTypes.Role, "Author"),
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