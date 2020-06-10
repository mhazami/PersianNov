using System;
using System.Collections.Generic;
using System.Text;
using Radyn.Framework;

namespace PersianNov.DataStructure
{
    [Serializable]
    [Schema("Book")]
    public sealed class AuthorBook : DataStructureBase<AuthorBook>
    {
        public Int32 _authorId;
        [Key(false)]
        [DbType("int")]
        public Int32 AuthorId
        {
            get { return _authorId; }
            set { base.SetPropertyValue("AuthorId", value); }
        }
        [Assosiation(PropName = "AuthorId")]
        public Author Author { get; set; }



        public Int32 _bookId;
        [Key(false)]
        [DbType("int")]
        public Int32 BookId
        {
            get { return _bookId; }
            set { base.SetPropertyValue("BookId", value); }
        }
        [Assosiation(PropName = "BookId")]
        public Book Book { get; set; }


    
    }
}
