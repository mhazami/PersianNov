using Microsoft.AspNetCore.Http;
using PersianNov.DataStructure;
using Radyn.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersianNov.Services.Interface
{
    public interface IBookFacade : IBaseFacade<Book>
    {
        bool Insert(Book book, IFormFile image,IFormFile pdf);
    }
}
