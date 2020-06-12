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
        public async Task<bool> InsertAsync(Book book, IFormFile image,IFormFile pdf)
        {
            base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                if (image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        var file = new DataStructure.File()
                        {
                            FileName = image.FileName,
                            Extension = Path.GetExtension(image.FileName),
                            ContentType = image.ContentType,
                            Content = ms.ToArray(),
                            Size = ms.ToArray().Length / 1024
                        };

                        book.ImageFile = file;
                    }

                    if (!await new FileBO().InsertAsync(base.ConnectionHandler, book.ImageFile))
                    {
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                    }
                    book.Image = book.ImageFile.Id;
                }
                if (pdf != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        pdf.CopyTo(ms);
                        var file = new DataStructure.File()
                        {
                            FileName = pdf.FileName,
                            Extension = Path.GetExtension(pdf.FileName),
                            ContentType = pdf.ContentType,
                            Content = ms.ToArray(),
                            Size = ms.ToArray().Length / 1024
                        };

                        book.PdfFile = file;
                    }

                    if (!await new FileBO().InsertAsync(base.ConnectionHandler, book.PdfFile))
                    {
                        throw new Exception("خطایی در درج اطلاعات کتاب رخ داده است");
                    }
                    book.PDF = book.PdfFile.Id;
                }
                if (!await new BookBO().InsertAsync(base.ConnectionHandler, book))
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
