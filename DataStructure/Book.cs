using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("Book")]
    public sealed class Book : DataStructureBase<Book>
    {
        public Int32 _id;
        [Key(true)]
        [DbType("int")]
        public Int32 Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
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

        public string _publisherName;
        [DbType("nvarchar(100)")]
        public string PublisherName
        {
            get { return _publisherName; }
            set { base.SetPropertyValue("PublisherName", value); }
        }

        public decimal _price;
        [DbType("decimal(8,3)")]
        public decimal Price
        {
            get { return _price; }
            set { base.SetPropertyValue("Price", value); }
        }

        public int _discount;
        [DbType("int")]
        public int Discount
        {
            get { return _discount; }
            set { base.SetPropertyValue("Discount", value); }
        }


        public string _publishDate;
        [DbType("varchar(10)")]
        public string PublishDate
        {
            get { return _publishDate; }
            set { base.SetPropertyValue("PublishDate", value); }
        }


        public int _pageCount;
        [DbType("int")]
        public int PageCount
        {
            get { return _pageCount; }
            set { base.SetPropertyValue("PageCount", value); }
        }

        public string _abstract;
        [DbType("nvarchar(Max)")]
        public string Abstract
        {
            get { return _abstract; }
            set { base.SetPropertyValue("Abstract", value); }
        }

        public int _percent;
        [DbType("int")]
        public int Percent
        {
            get { return _percent; }
            set { base.SetPropertyValue("Percent", value); }
        }


        public string _subject;
        [DbType("nvarchar(Max)")]
        public string Subject
        {
            get { return _subject; }
            set { base.SetPropertyValue("Subject", value); }
        }


        public string _janre;
        [DbType("nvarchar(50)")]
        public string Janre
        {
            get { return _janre; }
            set { base.SetPropertyValue("Janre", value); }
        }

        public bool _finished;
        [DbType("bit")]
        public bool Finished
        {
            get { return _finished; }
            set { base.SetPropertyValue("Finished", value); }
        }

        public bool _enabled;
        [DbType("bit")]
        public bool Enabled
        {
            get { return _enabled; }
            set { base.SetPropertyValue("Enabled", value); }
        }

        public bool _freeStydy;
        [DbType("bit")]
        public bool FreeStydy
        {
            get { return _freeStydy; }
            set { base.SetPropertyValue("FreeStydy", value); }
        }


        public Guid? _pDF;
        [DbType("uniqueidentifier")]
        public Guid? PDF
        {
            get { return _pDF; }
            set { base.SetPropertyValue("PDF", value); }
        }
        [Assosiation(PropName = "PDF")]
        public File PdfFile { get; set; }


        public Guid? _image;
        [DbType("uniqueidentifier")]
        public Guid? Image
        {
            get { return _image; }
            set { base.SetPropertyValue("Image", value); }
        }
        [Assosiation(PropName = "Image")]
        public File ImageFile { get; set; }


        public string _name;
        [DbType("nvarchar(100)")]
        public string Name
        {
            get { return _name; }
            set { base.SetPropertyValue("Name", value); }
        }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.Name; }
        }
    }
}
