using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;
using static PersianNov.DataStructure.Tools.Enums;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("Payment")]
    public sealed class Order : DataStructureBase<Order>
    {

        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        private Guid _customerId;
        [DbType("uniqueidentifier")]
        public Guid CustomerId
        {
            get { return _customerId; }
            set { base.SetPropertyValue("CustomerId", value); }
        }
        [Assosiation(PropName = "CustomerId")]
        public Customer Customer { get; set; }




        private Guid _bookId;
        [DbType("uniqueidentifier")]
        public Guid BookId
        {
            get { return _bookId; }
            set { base.SetPropertyValue("BookId", value); }
        }
        [Assosiation(PropName = "BookId")]
        public Book Book { get; set; }


        private long _number;
        [DbType("bigint")]
        public long Number
        {
            get { return _number; }
            set { base.SetPropertyValue("Number", value); }
        }

        private string _orderDate;
        [DbType("varchar(10)")]
        public string OrderDate
        {
            get { return _orderDate; }
            set { base.SetPropertyValue("OrderDate", value); }
        }

        private decimal _amount;
        [DbType("decimal(8,3)")]
        public decimal Amount
        {
            get { return _amount; }
            set { base.SetPropertyValue("Amount", value); }
        }

        private int _discount;
        [DbType("int")]
        public int Discount
        {
            get { return _discount; }
            set { base.SetPropertyValue("Discount", value); }
        }

        private decimal _totalAmount;
        [DbType("decimal(8,3)")]
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { base.SetPropertyValue("TotalAmount", value); }
        }

        private PaymentStatus _status;
        [DbType("tinyint")]
        public PaymentStatus Status
        {
            get { return _status; }
            set { base.SetPropertyValue("Status", value); }
        }

    }
}
