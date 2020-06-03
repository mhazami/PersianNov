using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.DataAccess
{
    public sealed class TaskMoneyDA : DALBase<TaskMoney>
    {
        public TaskMoneyDA(IConnectionHandler connectionHandler) : base(connectionHandler) { }
        internal class TaskMoneyCommandBuilder { }
    }
}
