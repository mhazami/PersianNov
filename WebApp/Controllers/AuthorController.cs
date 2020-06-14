using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersianNov.Services;
using PersianNov.DataStructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Author.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Cartable()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(user))
            {
                return Redirect("/Author/Login");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var author = await PersianNovComponent.Instance.AuthorFacade.Login(username, password);
                if (author != null)
                {
                    this.SetCookie(author);
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

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        public IActionResult Register()
        {
            ViewBag.Message = "";
            return View(new PersianNov.DataStructure.Author());
        }



        [HttpPost]
        public async Task<IActionResult> Register(PersianNov.DataStructure.Author author)
        {
            try
            {
                if (await PersianNovComponent.Instance.AuthorFacade.InsertAsync(author))
                {
                    this.SetCookie(author);
                    return Redirect("/");

                }
                else
                {
                    ViewBag.Message = "خطایی در زمان ثبت نام رخ داده است لطفا بعدا مجدد تلاش کنید"; ;
                    return View(author);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(author);
            }
        }

        private async void SetCookie(PersianNov.DataStructure.Author author)
        {
            var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, $"{author.FirstName} {author.LastName}"),
                     new Claim(ClaimTypes.Email,author.Email),
                     new Claim("Id",author.Id.ToString()),
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