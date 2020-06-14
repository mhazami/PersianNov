using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("Book")]
    public sealed class PublisherBook : DataStructureBase<PublisherBook>
    {
        public Guid _publisherId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid PublisherId
        {
            get { return _publisherId; }
            set { base.SetPropertyValue("PublisherId", value); }
        }
        [Assosiation(PropName = "PublisherId")]
        public Publisher Publisher { get; set; }



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



    }
}
