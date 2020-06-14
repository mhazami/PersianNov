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
        public Guid _authorId;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid AuthorId
        {
            get { return _authorId; }
            set { base.SetPropertyValue("AuthorId", value); }
        }
        [Assosiation(PropName = "AuthorId")]
        public Author Author { get; set; }



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
