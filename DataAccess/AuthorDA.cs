using System;
using System.Collections.Generic;
using System.Text;
using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace PersianNov.DataAccess
{
    public sealed class AuthorDA : DALBase<Author>
    {
        public AuthorDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class AuthorCommandBuilder { }
    }
}
