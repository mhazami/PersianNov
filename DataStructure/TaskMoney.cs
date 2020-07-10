using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;
using static PersianNov.DataStructure.Tools.Enums;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class TaskMoney : DataStructureBase<TaskMoney>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        private Guid _authorId;
        [DbType("uniqueidentifier")]
        public Guid AuthorId
        {
            get { return _authorId; }
            set { base.SetPropertyValue("AuthorId", value); }
        }
        [Assosiation(PropName = "AuthorId")]
        public Author Author { get; set; }


        private string _registerDate;
        [DbType("varchar(10)")]
        public string RegisterDate
        {
            get { return _registerDate; }
            set { base.SetPropertyValue("RegisterDate", value); }
        }


        private long _number;
        [DbType("bigint")]
        public long Number
        {
            get { return _number; }
            set { base.SetPropertyValue("Number", value); }
        }

        private decimal _amount;
        [DbType("decimal(18,3)")]
        public decimal Amount
        {
            get { return _amount; }
            set { base.SetPropertyValue("Amount", value); }
        }



        private PaymentTaskStatus _status;
        [DbType("tinyint")]
        public PaymentTaskStatus Status
        {
            get { return _status; }
            set { base.SetPropertyValue("Status", value); }
        }




    }
}
