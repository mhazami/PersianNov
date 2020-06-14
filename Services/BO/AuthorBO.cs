using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersianNov.Services.BO
{
    public sealed class AuthorBO : BusinessBase<Author>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Author obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
        protected override void CheckConstraint(IConnectionHandler connectionHandler, Author item)
        {
            if (string.IsNullOrEmpty(item.Email))
                throw new Exception("لطفا ایمیل خود را وارد کنید");
            if (string.IsNullOrEmpty(item.FirstName))
                throw new Exception("لطفا نام خود را وارد کنید");
            if (string.IsNullOrEmpty(item.Password))
                throw new Exception("لطفا رمز عبور خود را وارد کنید");
            if (!Utils.IsEmail(item.Email))
                throw new Exception("لطفا ایمیل خود را صحیح وارد کنید");

            item.Username = item.Email;
            base.CheckConstraint(connectionHandler, item);
        }

        internal bool CheckBookOwner(IConnectionHandler connectionHandler, Guid authorId, Guid bookId)
        {
            return new BookBO().Any(connectionHandler, x => x.AuthorId == authorId && x.Id == bookId);
        }

        public override Task<bool> InsertAsync(IConnectionHandler connectionHandler, Author obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            var exist = base.Any(connectionHandler, x => x.Email.ToLower() == obj.Email.ToLower());
            if (exist)
                throw new Exception("کابر دیگری با این ایمیل در سیستم موجود میباشد ");
            if (obj.Password != obj.RepeatPassword)
                throw new Exception("رمز عبور و تکرار آن مطابقت ندارند");
            obj.Password = StringUtils.HashPassword(obj.Password);
            return base.InsertAsync(connectionHandler, obj);
        }

        public override Task<bool> UpdateAsync(IConnectionHandler connectionHandler, Author obj)
        {
            var old = base.GetAsync(connectionHandler, obj.Id);
            if (!string.IsNullOrEmpty(obj.Password))
            {
                if (obj.Password != obj.RepeatPassword)
                    throw new Exception("رمز عبور و تکرار آن مطابقت ندارند");
                if (StringUtils.HashPassword(obj.Password) != obj.Password)
                    obj.Password = StringUtils.HashPassword(obj.Password);

            }
            return base.UpdateAsync(connectionHandler, obj);
        }

        public async Task<Author> Login(IConnectionHandler connectionHandler, string username, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("رمز غبور را وارد کنید");
            if (string.IsNullOrEmpty(username))
                throw new Exception("نام کاربری یا ایمیل خود را وارد کنید");

            return await base.FirstOrDefaultAsync(connectionHandler, x => x.Username == username ||
            x.Email == username && x.Password == StringUtils.HashPassword(password));
        }
    }
}
