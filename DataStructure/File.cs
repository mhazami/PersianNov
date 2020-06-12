using Radyn.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("FileManager")]
    public sealed class File : DataStructureBase<File>
    {
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id { get; set; }

        [IsNullable]
        [DbType("nvarchar(150)")]
        public string FileName { get; set; }

        [DbType("nvarchar(500)")]
        public string ContentType { get; set; }

        [DbType("varchar(10)")]
        public string Extension { get; set; }

        [DbType("varbinary(max)")]
        public byte[] Content { get; set; }

        [DbType("int")]
        public int Size { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public long MaxSize { get; set; }

   
        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public string FullName
        {
            get { return this.FileName + "." + this.Extension; }
        }
    }
}
