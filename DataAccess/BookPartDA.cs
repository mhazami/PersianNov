using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.DataAccess
{
    public sealed class BookPartDA : DALBase<BookPart>
    {
        public BookPartDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class BookPartCommandBuilder { }
    }
}
