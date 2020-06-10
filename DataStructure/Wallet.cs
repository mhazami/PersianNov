using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class Wallet : DataStructureBase<Wallet>
    {
        public Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        public bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }


        public decimal _amount;
        [DbType("decimal(8,3)")]
        public decimal Amount
        {
            get { return _amount; }
            set { base.SetPropertyValue("Amount", value); }
        }


        public decimal _input;
        [DbType("decimal(8,3)")]
        public decimal Input
        {
            get { return _input; }
            set { base.SetPropertyValue("Input", value); }
        }


        public decimal _output;
        [DbType("decimal(8,3)")]
        public decimal Output
        {
            get { return _output; }
            set { base.SetPropertyValue("Output", value); }
        }


        public string _changeDate;
        [DbType("varchar(10)")]
        public string ChangeDate
        {
            get { return _changeDate; }
            set { base.SetPropertyValue("ChangeDate", value); }
        }


        public string _number;
        [DbType("varchar(10)")]
        public string Number
        {
            get { return _number; }
            set { base.SetPropertyValue("Number", value); }
        }


        public Int32? _authorId;
        [DbType("int")]
        public Int32? AuthorId
        {
            get { return _authorId; }
            set { base.SetPropertyValue("AuthorId", value); }
        }
        [Assosiation(PropName = "AuthorId")]
        public Author Author { get; set; }

        public Int32? _publisherId;
        [DbType("int")]
        public Int32? PublisherId
        {
            get { return _publisherId; }
            set { base.SetPropertyValue("PublisherId", value); }
        }
        [Assosiation(PropName = "PublisherId")]
        public Publisher Publisher { get; set; }




   
    }
}
