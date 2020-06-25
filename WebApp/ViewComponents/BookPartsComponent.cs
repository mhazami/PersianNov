using Microsoft.AspNetCore.Mvc;
using PersianNov.DataStructure;
using PersianNov.Services;
using Radyn.Framework;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WebApp.ViewComponents
{
    public class BookPartsComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid bookId, Guid? id, string type)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

            PredicateBuilder<BookPart> query = new PredicateBuilder<BookPart>();
            query.And(x => x.Enabled && x.Approve);
            if (string.IsNullOrEmpty(userId))
                query.And(x => !x.VIP);
            else
            {
                var bougth = PersianNovComponent.Instance.CustomerBookFacade.Any(x => x.CustomerId == userId.ToGuid() && x.BookId == bookId);
                if (!bougth)
                    query.And(x => !x.VIP);
            }
            if (id != null)
            {
                var part = PersianNovComponent.Instance.BookPartFacade.Get(id);
                switch (type)
                {
                    case "next":
                        query.And(x => x.PartNumber > part.PartNumber);
                        break;
                    case "per":
                        query.And(x => x.PartNumber < part.PartNumber);
                        break;
                }
            }
            BookPart result = null;
            if (string.IsNullOrEmpty(type) || type == "next")
                result = await PersianNovComponent.Instance.BookPartFacade.FirstOrDefaultWithOrderByAsync(x => x.PartNumber, query.GetExpression());
            else
                result = await PersianNovComponent.Instance.BookPartFacade.FirstOrDefaultWithOrderByDescendingAsync(x => x.PartNumber, query.GetExpression());
            if (result == null)
                result = PersianNovComponent.Instance.BookPartFacade.Get(id);
            return await Task.FromResult((IViewComponentResult)View("BookParts", result));
        }



    }
}
