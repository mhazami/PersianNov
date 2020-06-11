using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.Services.BO
{
    public sealed class AuthorBO : BusinessBase<Author>
    {
        internal Author Login(IConnectionHandler connectionHandler, string username, string password)
        {
            return base.FirstOrDefault(connectionHandler,
                x => x.Username == username ||
                x.Email == username &&
                x.Password == StringUtils.HashPassword(password));
            
        }
    }
}
