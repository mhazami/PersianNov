using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;
using static PersianNov.DataStructure.Tools.Enums;

namespace PersianNov.DataStructure
{

    [Serializable]
    [Schema("Payment")]
    public sealed class Payment : DataStructureBase<Payment>
    {
        public Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        public Int32? _customerId;
        [DbType("int")]
        public Int32? CustomerId
        {
            get { return _customerId; }
            set { base.SetPropertyValue("CustomerId", value); }
        }
        [Assosiation(PropName = "CustomerId")]
        public Customer Customer { get; set; }

        public Int32? _publisherId;
        [DbType("int")]
        public Int32? PublisherId
        {
            get { return _publisherId; }
            set { base.SetPropertyValue("PublisherId", value); }
        }
        [Assosiation(PropName = "PublisherId")]
        public Publisher Publisher { get; set; }


        public Int32? _authorId;
        [DbType("int")]
        public Int32? AuthorId
        {
            get { return _authorId; }
            set { base.SetPropertyValue("AuthorId", value); }
        }
        [Assosiation(PropName = "AuthorId")]
        public Author Author { get; set; }


        public Int32? _orderId;
        [DbType("int")]
        public Int32? OrderId
        {
            get { return _orderId; }
            set { base.SetPropertyValue("OrderId", value); }
        }
        [Assosiation(PropName = "OrderId")]
        public Order Order { get; set; }


        public string _number;
        [DbType("nvarchar(10)")]
        public string Number
        {
            get { return _number; }
            set { base.SetPropertyValue("Number", value); }
        }


        public decimal _amount;
        [DbType("decimal(8,3)")]
        public decimal Amount
        {
            get { return _amount; }
            set { base.SetPropertyValue("Amount", value); }
        }

        public string _paymentDate;
        [DbType("varchar(10)")]
        public string PaymentDate
        {
            get { return _paymentDate; }
            set { base.SetPropertyValue("PaymentDate", value); }
        }

        public PaymentStatus _paymentStatus;
        [DbType("tinyint")]
        public PaymentStatus PaymentStatus
        {
            get { return _paymentStatus; }
            set { base.SetPropertyValue("PaymentStatus", value); }
        }


        public PaymentType _paymentType;
        [DbType("tinyint")]
        public PaymentType PaymentType
        {
            get { return _paymentType; }
            set { base.SetPropertyValue("PaymentType", value); }
        }



        public Guid? _imageBankDoc;
        [DbType("uniqueidentifier")]
        public Guid? ImageBankDoc
        {
            get { return _imageBankDoc; }
            set { base.SetPropertyValue("ImageBankDoc", value); }
        }
        [Assosiation(PropName = "ImageBankDoc")]
        public File ImageBankDocFile { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Number; }
        }
    }
}
