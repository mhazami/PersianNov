using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;
using Radyn.Utility;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class Wallet : DataStructureBase<Wallet>
    {
        public Wallet()
        {
            Enabled = true;
            Input = 0;
            Output = 0;
            Amount = 0;
            ChangeDate = DateTime.Now.ShamsiDate();
        }

        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }


        private decimal _amount;
        [DbType("decimal(8,3)")]
        public decimal Amount
        {
            get { return _amount; }
            set { base.SetPropertyValue("Amount", value); }
        }


        private decimal _input;
        [DbType("decimal(8,3)")]
        public decimal Input
        {
            get { return _input; }
            set { base.SetPropertyValue("Input", value); }
        }


        private decimal _output;
        [DbType("decimal(8,3)")]
        public decimal Output
        {
            get { return _output; }
            set { base.SetPropertyValue("Output", value); }
        }


        private string _changeDate;
        [DbType("varchar(10)")]
        public string ChangeDate
        {
            get { return _changeDate; }
            set { base.SetPropertyValue("ChangeDate", value); }
        }


        private long _number;
        [DbType("bigint")]
        public long Number
        {
            get { return _number; }
            set { base.SetPropertyValue("Number", value); }
        }


        private Guid? _authorId;
        [DbType("int")]
        public Guid? AuthorId
        {
            get { return _authorId; }
            set { base.SetPropertyValue("AuthorId", value); }
        }
        [Assosiation(PropName = "AuthorId")]
        public Author Author { get; set; }

        private Guid? _publisherId;
        [DbType("uniqueidentifier")]
        public Guid? PublisherId
        {
            get { return _publisherId; }
            set { base.SetPropertyValue("PublisherId", value); }
        }
        [Assosiation(PropName = "PublisherId")]
        public Publisher Publisher { get; set; }




   
    }
}
