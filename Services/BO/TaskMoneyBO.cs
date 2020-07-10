using PersianNov.DataStructure;
using PersianNov.DataStructure.Tools;
using PersianNov.Services.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.Services.BO
{
    public sealed class TaskMoneyBO : BusinessBase<TaskMoney>
    {
        public override bool Insert(IConnectionHandler connectionHandler, TaskMoney obj)
        {
            var input = new WalletBO().Sum(connectionHandler, x => x.Input, x => x.AuthorId == obj.AuthorId);
            if (input.HasValue && input > 0)
            {
                var output = new WalletBO().Sum(connectionHandler, x => x.Output, x => x.AuthorId == obj.AuthorId);
                var dif = input - output;
                if (dif > 0 && obj.Amount <= dif)
                {
                    obj.RegisterDate = DateTime.Now.ShamsiDate();
                    obj.Id = Guid.NewGuid();
                    obj.Status = Enums.PaymentTaskStatus.Prossecing;
                    obj.Number = new Genetator<TaskMoney>().NumberGenerator(x => x.Number);
                    return base.Insert(connectionHandler, obj);
                }
            }
            throw new KnownException("موجودی شما کافی نیست");

        }
    }
}
