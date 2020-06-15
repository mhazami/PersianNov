using PersianNov.DataStructure;
using PersianNov.Services.BO;
using PersianNov.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersianNov.Services.Facade
{
    public sealed class AuthorFacade : PersianNovBaseFacade<Author>, IAuthorFacade
    {
        public bool CheckBookOwner(Guid authorId, Guid bookId)
        {
            return new AuthorBO().CheckBookOwner(ConnectionHandler, authorId, bookId);
        }

        public Author Login(string username, string password)
        {
            return new AuthorBO().Login(base.ConnectionHandler, username, password);
        }

        public bool UpdatePassword(Author author)
        {
            return new AuthorBO().UpdatePassword(base.ConnectionHandler, author);
        }
    }
}
