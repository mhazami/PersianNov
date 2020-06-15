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
		private Guid _id;
		[Key(false)]
		[DbType("uniqueidentifier")]
		public Guid Id
		{
			get { return _id; }
			set { base.SetPropertyValue("Id", value); }
		}

		private string _name;
		[IsNullable]
		[DbType("nvarchar(150)")]
		public string Name
		{
			get { return _name; }
			set { base.SetPropertyValue("Name", value); }
		}

		private Int32 _partNumber;
		[DbType("int")]
		public Int32 PartNumber
		{
			get { return _partNumber; }
			set { base.SetPropertyValue("PartNumber", value); }
		}

		private Guid? _image;
		[DbType("uniqueidentifier")]
		public Guid? Image
		{
			get { return _image; }
			set { base.SetPropertyValue("Image", value); }
		}
		[Assosiation(PropName = "Image")]
		public File ImageFile { get; set; }

		private string _publishDate;
		[IsNullable]
		[DbType("varchar(10)")]
		public string PublishDate
		{
			get { return _publishDate; }
			set { base.SetPropertyValue("PublishDate", value); }
		}

		private bool _enabled;
		[DbType("bit")]
		public bool Enabled
		{
			get { return _enabled; }
			set { base.SetPropertyValue("Enabled", value); }
		}

		private bool? _vIP;
		[IsNullable]
		[DbType("bit")]
		public bool? VIP
		{
			get { return _vIP; }
			set { base.SetPropertyValue("VIP", value); }
		}

		private bool? _approve;
		[IsNullable]
		[DbType("bit")]
		public bool? Approve
		{
			get { return _approve; }
			set { base.SetPropertyValue("Approve", value); }
		}

		private Guid _bookId;
		[DbType("uniqueidentifier")]
		public Guid BookId
		{
			get
			{
				return this._bookId;
			}
			set
			{
				base.SetPropertyValue("BookId", value);
			}
		}
		[Assosiation(PropName = "BookId")]
		public Book Book { get; set; }



		private string _text;
		[IsNullable]
		[DbType("ntext")]
		public string Text
		{
			get { return _text; }
			set { base.SetPropertyValue("Text", value); }
		}


	}
}
