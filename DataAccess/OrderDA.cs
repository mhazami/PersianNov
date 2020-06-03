using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.DataAccess
{
    public sealed class OrderDA : DALBase<Order>
    {
        public OrderDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class OrderCommandBuilder { }
    }
}
