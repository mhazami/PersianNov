using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("Book")]
    public sealed class BookPart : DataStructureBase<BookPart>
    {
        public Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        public string _name;
        [DbType("nvarchar(150)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }

        public Guid _bookId;
        [DbType("uniqueidentifier")]
        public Guid BookId
        {
            get { return _bookId; }
            set { base.SetPropertyValue("BookId", value); }
        }
        [Assosiation(PropName = "BookId")]
        public Book Book { get; set; }

        public int _partNumber;
        [DbType("int")]
        public int PartNumber
        { 
            get { return _partNumber; }
            set { base.SetPropertyValue("PartNumber", value); }
        }

        public Guid? _image;
        [DbType("uniqueidentifier")]
        public Guid? Image
        {
            get { return _image; }
            set { base.SetPropertyValue("Image", value); }
        }
        [Assosiation(PropName = "Image")]
        public File ImageFile { get; set; }

        public string _publishDate;
        [DbType("varchar(10)")]
        public string PublishDate
        {
            get { return _publishDate; }
            set { base.SetPropertyValue("PublishDate", value); }
        }

        private bool _approve;
        [DbType("bit")]
        public bool Approve
        {
            get { return _approve; }
            set { base.SetPropertyValue("Approve", value); }
        }

        private bool _vIP;
        [DbType("bit")]
        public bool VIP
        {
            get { return _vIP; }
            set { base.SetPropertyValue("VIP", value); }
        }

        public bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

  
    }
}
