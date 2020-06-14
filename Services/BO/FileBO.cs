using PersianNov.DataAccess;
using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using System;
using PersianNov.Services.CacheManager;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PersianNov.Services.BO
{
    public sealed class FileBO : BusinessBase<DataStructure.File>
    {
        public override bool Insert(IConnectionHandler connectionHandler, DataStructure.File obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            var da = new FileDA(connectionHandler);
            return da.Insert(obj) > 0;
        }

        public override DataStructure.File Get(IConnectionHandler connectionHandler, params object[] keys)
        {
            var file = FileCacheManager.FileCache.GetItem(keys[0].ToString().ToGuid());
            if (file == null)
            {
                file = base.Get(connectionHandler, keys);
                if (file != null)
                    FileCacheManager.FileCache.AddItem(file);
            }
            return file;

        }

        public override bool Delete(IConnectionHandler connectionHandler, params object[] keys)
        {
            FileCacheManager.FileCache.RemoveItem(keys[0].ToString().ToGuid());
            return base.Delete(connectionHandler, keys);
        }

        public Guid Insert(IConnectionHandler connectionHandler, IFormFile file)
        {
            try
            {
                var picture = new byte[file.Length];
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyToAsync(memoryStream);
                    picture = memoryStream.ToArray();
                }

                var fileContent = new DataStructure.File
                {
                    Content = picture,
                    ContentType = file.ContentType,
                    Extension = Path.GetExtension(file.FileName),
                    FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName),

                };

                return base.Insert(connectionHandler, fileContent) ? fileContent.Id : Guid.Empty;
            }

            catch (KnownException knownException)
            {
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                throw new KnownException(ex.Message, ex);
            }

        }
        public Guid Insert(IConnectionHandler connectionHandler, IFormFile file, DataStructure.File fileoptions)
        {
            try
            {

                var picture = new byte[file.Length];
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyToAsync(memoryStream);
                    picture = memoryStream.ToArray();
                }
                var fileContent = new DataStructure.File
                {
                    Content = picture,
                    ContentType = file.ContentType,
                    Extension =Path.GetExtension(file.FileName),
                    FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName),

                };
                if (fileoptions != null)
                {
                    //if (fileoptions.MaxSize > 0)
                    //{
                    //    var i = file.ContentLength / 1024;
                    //    if (i > fileoptions.MaxSize)
                    //        throw new Exception(Resources.CRM.Filesizeislargerthanallowedsize);

                    //}
                    if (!string.IsNullOrEmpty(fileoptions.FileName))
                        fileContent.FileName = fileoptions.FileName;
                  
                }

                if (!this.Insert(connectionHandler, fileContent))
                    throw new Exception("خطایی ذخیره فایل وجود دارد");
                return fileContent.Id;

            }
            catch (KnownException knownException)
            {
                throw new KnownException(knownException.Message, knownException);
            }
            catch (Exception ex)
            {
                throw new KnownException(ex.Message, ex);
            }

        }
        public override bool Update(IConnectionHandler connectionHandler, DataStructure.File obj)
        {
            var da = new FileDA(connectionHandler);
            if (da.Update(obj) <= 0) return false;
            FileCacheManager.FileCache.RemoveItem(obj.Id);
            return true;
        }
    }
}
