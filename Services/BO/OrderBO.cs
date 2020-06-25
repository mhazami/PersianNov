using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.Services.BO
{
    public sealed class OrderBO : BusinessBase<Order>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Order obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
