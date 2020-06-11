using PersianNov.DataStructure;
using PersianNov.Services.BO;
using PersianNov.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.Services.Facade
{
    public sealed class AuthorFacade : PersianNovBaseFacade<Author>, IAuthorFacade
    {
        public Author Login(string username, string password)
        {
            return new AuthorBO().Login(base.ConnectionHandler, username, password);
        }
    }
}
