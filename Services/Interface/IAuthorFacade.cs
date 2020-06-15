using PersianNov.DataStructure;
using Radyn.Framework;
using System;
using System.Threading.Tasks;

namespace PersianNov.Services.Interface
{
    public interface IAuthorFacade : IBaseFacade<Author>
    {
        Author Login(string username, string password);

        bool CheckBookOwner(Guid authorId, Guid bookId);
        bool ForgotPassword(string email);
        bool UpdatePassword(Author author);
    }
}
