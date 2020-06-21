using Microsoft.AspNetCore.Http;
using PersianNov.DataStructure;
using PersianNov.DataStructure.Tools;
using PersianNov.Services.BO;
using PersianNov.Services.Interface;
using Radyn.Framework;
using Radyn.Utility;
using System;
using System.Data;

namespace PersianNov.Services.Facade
{
    public sealed class BookPartFacade : PersianNovBaseFacade<BookPart>, IBookPartFacade
    {
        public override bool Insert(BookPart bookPart)
        {
            base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                var file = new File()
                {
                    Content = bookPart.Text.ConvertTextToHtml(),
                    ContentType = "image/png",
                    Extension = ".png",
                    FileName = bookPart.Name,
                    Id = Guid.NewGuid(),
                };
                var imageId = new FileBO().InsertFile(base.ConnectionHandler, file);
                if (imageId == null && imageId == Guid.Empty)
                {
                    throw new KnownException("خطایی در درج اطلاعات کتاب رخ داده است");
                }
                bookPart.Image = imageId;

                bookPart.Id = Guid.NewGuid();
                bookPart.PublishDate = DateTime.Now.ShamsiDate();
                if (!new BookPartBO().Insert(base.ConnectionHandler, bookPart))
                {
                    throw new KnownException("خطایی در درج اطلاعات کتاب رخ داده است");
                }
                base.ConnectionHandler.CommitTransaction();
                return true;

            }
            catch (Exception ex)
            {
                base.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Update(BookPart bookPart)
        {
            base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            var fileBO = new FileBO();
            try
            {
                var file = new File()
                {
                    Content = bookPart.Text.ConvertTextToHtml(),
                    Extension = ".png",
                    ContentType = "image/png",
                    FileName = bookPart.Name,
                };
                if (bookPart.Image != null)
                    if (!fileBO.Delete(base.ConnectionHandler, bookPart.Image))
                        throw new KnownException("خطایی در درج اطلاعات کتاب رخ داده است");
                var imageId = fileBO.InsertFile(base.ConnectionHandler, file);
                if (imageId == null && imageId == Guid.Empty)
                    throw new KnownException("خطایی در درج اطلاعات کتاب رخ داده است");

                bookPart.Image = imageId;

                if (!new BookPartBO().Update(base.ConnectionHandler, bookPart))
                    throw new KnownException("خطایی در درج اطلاعات کتاب رخ داده است");

                base.ConnectionHandler.CommitTransaction();
                return true;

            }
            catch (Exception ex)
            {
                base.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool Delete(Guid id)
        {
            base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            var fileBO = new FileBO();
            var bookPartBO = new BookPartBO();
            try
            {
                var bookPart = bookPartBO.Get(base.ConnectionHandler, id);
                if (bookPart.Image != null && bookPart.Image != Guid.Empty)
                    if (!fileBO.Delete(base.ConnectionHandler, bookPart.Image))
                        throw new Exception("خطایی در حذف اطلاعات کتاب رخ داده است");
                if (!bookPartBO.Delete(base.ConnectionHandler, bookPart))
                    throw new Exception("خطایی در حذف اطلاعات کتاب رخ داده است");

                base.ConnectionHandler.CommitTransaction();
                return true;

            }
            catch (Exception ex)
            {
                base.ConnectionHandler.RollBack();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
