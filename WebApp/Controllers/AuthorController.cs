using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersianNov.DataStructure;
using PersianNov.Services;

namespace WebApp.Controllers
{
    public class AuthorController : Controller
    {
        //public async Task<IActionResult> Index()
        //{
        //    var list = await PersianNovComponent.Instance.AuthorFacade.GetAllAsync();
        //    return View(list);
        //}


        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            Author author =await PersianNovComponent.Instance.AuthorFacade.Login(username, password);
            if (author != null)
            {
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, $"{author.FirstName} {author.LastName}"),
                     new Claim(ClaimTypes.Email,author.Email),
                     new Claim("Username", author.Username),
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
                return Redirect("Cartable");
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("Login");
        }
    }
}