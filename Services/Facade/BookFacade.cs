using Microsoft.AspNetCore.Http;
using PersianNov.DataStructure;
using PersianNov.Services.BO;
using PersianNov.Services.Interface;
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
    }
}
