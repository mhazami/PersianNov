using Radyn.Framework;
using System;
using System.Net.Http.Headers;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("FileManager")]
    public sealed class File : DataStructureBase<File>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

        private string _fileName;
        [IsNullable]
        [DbType("nvarchar(150)")]
        public string FileName
        {
            get { return _fileName; }
            set { base.SetPropertyValue("FileName", value); }
        }

        private string _contentType;
        [DbType("nvarchar(500)")]
        public string ContentType
        {
            get { return _contentType; }
            set { base.SetPropertyValue("ContentType", value); }
        }

        private string _extension;
        [DbType("varchar(10)")]
        public string Extension
        {
            get { return _extension; }
            set { base.SetPropertyValue("Extension", value); }
        }

        private byte[] _content;
        [DbType("varbinary(max)")]
        public byte[] Content
        {
            get { return _content; }
            set { base.SetPropertyValue("Content", value); }
        }


        private int _size;
        [DbType("int")]
        public int Size
        {
            get
            {
                return _size;
            }
            set { base.SetPropertyValue("Size", value); }
        }


    }
}
