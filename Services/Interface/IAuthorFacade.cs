using PersianNov.DataStructure;
using Radyn.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersianNov.Services.Interface
{
    public interface IAuthorFacade : IBaseFacade<Author>
    {
        Task<Author> Login(string username, string password);

        bool CheckBookOwner(Guid authorId, Guid bookId);
    }
}
