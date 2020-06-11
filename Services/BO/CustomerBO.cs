using PersianNov.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PersianNov.Services.BO
{
    public sealed class CustomerBO : BusinessBase<Customer>
    {
        protected override void CheckConstraint(IConnectionHandler connectionHandler, Customer item)
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

        public override Task<bool> InsertAsync(IConnectionHandler connectionHandler, Customer obj)
        {
            if (obj.Password != obj.RepeatPassword)
                throw new Exception("رمز عبور و تکرار آن مطابقت ندارند");
            obj.Password = StringUtils.HashPassword(obj.Password);
            return base.InsertAsync(connectionHandler, obj);
        }

        public override Task<bool> UpdateAsync(IConnectionHandler connectionHandler, Customer obj)
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

        public async Task<Customer> Login(IConnectionHandler connectionHandler, string username, string password)
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
