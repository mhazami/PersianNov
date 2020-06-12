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
        public async Task<Author> Login(string username, string password)
        {
            return await new AuthorBO().Login(base.ConnectionHandler, username, password);
        }
    }
}
