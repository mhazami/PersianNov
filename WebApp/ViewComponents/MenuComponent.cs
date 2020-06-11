using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.ViewComponents
{
    public class MenuComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.User= HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            return await Task.FromResult((IViewComponentResult)View("Menu"));
        }

    }
}
