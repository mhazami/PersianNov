using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("Book")]
    public sealed class CustomerBook : DataStructureBase<CustomerBook>
    {
        public Guid _customerId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid CustomerId
        {
            get { return _customerId; }
            set { base.SetPropertyValue("CustomerId", value); }
        }
        [Assosiation(PropName = "CustomerId")]
        public Customer Customer { get; set; }



        public Guid _bookId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid BookId
        {
            get { return _bookId; }
            set { base.SetPropertyValue("BookId", value); }
        }
        [Assosiation(PropName = "BookId")]
        public Book Book { get; set; }


        public string _number;
        [DbType("varchar(10)")]
        public string Number
        {
            get { return _number; }
            set { base.SetPropertyValue("Number", value); }
        }

        public bool _vIP;
        [DbType("bit")]
        public bool VIP
        {
            get { return _vIP; }
            set { base.SetPropertyValue("VIP", value); }
        }



 
    }
}
