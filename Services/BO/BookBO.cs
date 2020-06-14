using Microsoft.AspNetCore.Http;
using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PersianNov.Services.BO
{
    public sealed class BookBO : BusinessBase<Book>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Book obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
        protected override void CheckConstraint(IConnectionHandler connectionHandler, Book item)
        {
            if (string.IsNullOrEmpty(item.Name))
                throw new Exception("لطفا عنوان کتاب را وارد کنید");
            if (item.AuthorId == null)
                throw new Exception("خطایی در زما درج اطلاعات رخ داده است");
            if (string.IsNullOrEmpty(item.Subject))
                throw new Exception("لطفا موضوع کتاب را مشخص نمایید");
            if (item.PDF != null && item.PDF != Guid.Empty && (item.PdfFile == null || item.PdfFile.Extension.ToLower() != "pdf"))
                throw new Exception("فایل بارگزاری شده برای کتاب باید با پسوند pdf باشد");
            base.CheckConstraint(connectionHandler, item);
        }
    }
}
