using Microsoft.AspNetCore.Http;
using PersianNov.DataStructure;
using PersianNov.Services.BO;
using PersianNov.Services.Interface;
using System;
using System.Data;

namespace PersianNov.Services.Facade
{
    public sealed class BookPartFacade : PersianNovBaseFacade<BookPart>, IBookPartFacade
    {
        public bool Insert(BookPart bookPart, IFormFile image)
        {
            base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                if (image != null)
                {
                    var imageId = new FileBO().Insert(base.ConnectionHandler, image, bookPart.ImageFile);
                    if (imageId == null && imageId == Guid.Empty)
                    {
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                    }
                    bookPart.Image = imageId;
                }
                bookPart.Id = Guid.NewGuid();
                if (!new BookPartBO().Insert(base.ConnectionHandler, bookPart))
                {
                    throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                }
                base.ConnectionHandler.CommitTransaction();
                return true;

            }
            catch (Exception ex)
            {
                base.ConnectionHandler.RollBack();
                throw new Exception(ex.Message, ex);
            }
        }

        public bool Update(BookPart bookPart, IFormFile image)
        {
            base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            var fileBO = new FileBO();
            try
            {
                if (image != null)
                {
                    if (bookPart.Image != null)
                        if (!fileBO.Delete(base.ConnectionHandler, bookPart.Image))
                            throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                    var imageId = fileBO.Insert(base.ConnectionHandler, image, bookPart.ImageFile);
                    if (imageId == null && imageId == Guid.Empty)
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");

                    bookPart.Image = imageId;
                }
                if (!new BookPartBO().Update(base.ConnectionHandler, bookPart))
                    throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");

                base.ConnectionHandler.CommitTransaction();
                return true;

            }
            catch (Exception ex)
            {
                base.ConnectionHandler.RollBack();
                throw new Exception(ex.Message, ex);
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
