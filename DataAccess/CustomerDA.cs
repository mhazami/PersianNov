using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.DataAccess
{
    public sealed class CustomerDA : DALBase<Customer>
    {
        public CustomerDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class CustomerCommandBuilder { }
    }
}
