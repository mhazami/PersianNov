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

        public Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }


        public Int32 _customerId;
        [DbType("int")]
        public Int32 CustomerId
        {
            get { return _customerId; }
            set { base.SetPropertyValue("CustomerId", value); }
        }
        [Assosiation(PropName = "CustomerId")]
        public Customer Customer { get; set; }




        public Int32 _bookId;
        [DbType("int")]
        public Int32 BookId
        {
            get { return _bookId; }
            set { base.SetPropertyValue("BookId", value); }
        }
        [Assosiation(PropName = "BookId")]
        public Book Book { get; set; }


        public string _number;
        [DbType("nvarchar(10)")]
        public string Number
        {
            get { return _number; }
            set { base.SetPropertyValue("Number", value); }
        }

        public string _orderDate;
        [DbType("varchar(10)")]
        public string OrderDate
        {
            get { return _orderDate; }
            set { base.SetPropertyValue("OrderDate", value); }
        }

        public decimal _amount;
        [DbType("decimal(8,3)")]
        public decimal Amount
        {
            get { return _amount; }
            set { base.SetPropertyValue("Amount", value); }
        }

        public int _discount;
        [DbType("int")]
        public int Discount
        {
            get { return _discount; }
            set { base.SetPropertyValue("Discount", value); }
        }

        public decimal _totalAmount;
        [DbType("decimal(8,3)")]
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { base.SetPropertyValue("TotalAmount", value); }
        }

        public PaymentStatus _status;
        [DbType("tinyint")]
        public PaymentStatus Status
        {
            get { return _status; }
            set { base.SetPropertyValue("Status", value); }
        }



        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Number; }
        }
    }
}
