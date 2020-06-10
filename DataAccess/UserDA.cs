using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.DataAccess
{
    public sealed class UserDA : DALBase<User>
    {
        public UserDA(IConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }
    }
}
