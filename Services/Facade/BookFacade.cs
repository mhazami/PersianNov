using Microsoft.AspNetCore.Http;
using PersianNov.DataStructure;
using PersianNov.Services.BO;
using PersianNov.Services.Interface;
using Radyn.Framework.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PersianNov.Services.Facade
{
    public sealed class BookFacade : PersianNovBaseFacade<Book>, IBookFacade
    {

        public bool Insert(Book book, IFormFile image, IFormFile pdf)
        {
            base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                if (image != null)
                {
                    var imageId = new FileBO().Insert(base.ConnectionHandler, image, book.ImageFile);
                    if (imageId == null && imageId == Guid.Empty)
                    {
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                    }
                    book.Image = imageId;
                }
                if (pdf != null)
                {
                    var pdfId = new FileBO().Insert(base.ConnectionHandler, pdf, book.PdfFile);
                    if (pdfId == null && pdfId == Guid.Empty)
                    {
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                    }
                    book.PDF = pdfId;
                }
                if (!new BookBO().Insert(base.ConnectionHandler, book))
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

        public bool Update(Book book, IFormFile image, IFormFile pdf)
        {
            base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            var fileBO = new FileBO();
            try
            {
                if (image != null)
                {
                    if (!fileBO.Delete(base.ConnectionHandler, book.Image))
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                    var imageId = fileBO.Insert(base.ConnectionHandler, image, book.ImageFile);
                    if (imageId == null && imageId == Guid.Empty)
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");

                    book.Image = imageId;
                }
                if (pdf != null)
                {
                    if (!fileBO.Delete(base.ConnectionHandler, book.Image))
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                    var pdfId = fileBO.Insert(base.ConnectionHandler, pdf, book.PdfFile);
                    if (pdfId == null && pdfId == Guid.Empty)
                    {
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                    }
                    book.PDF = pdfId;
                }
                if (!new BookBO().Update(base.ConnectionHandler, book))
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
            var bookBO = new BookBO();
            try
            {
                var book = bookBO.Get(base.ConnectionHandler, id);
                if (book.Image != null && book.Image != Guid.Empty)
                    if (!fileBO.Delete(base.ConnectionHandler, book.Image))
                        throw new Exception("خطایی در حذف اطلاعات کتاب رخ داده است");
                if (book.PDF != null && book.PDF != Guid.Empty)
                    if (!fileBO.Delete(base.ConnectionHandler, book.PDF))
                        throw new Exception("خطایی در حذف اطلاعات کتاب رخ داده است");
                if (!bookBO.Delete(base.ConnectionHandler, book))
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

