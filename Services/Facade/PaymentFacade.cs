using PersianNov.DataStructure;
using PersianNov.Services.BO;
using PersianNov.Services.Interface;
using Radyn.Framework;
using Radyn.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static PersianNov.DataStructure.Tools.Enums;

namespace PersianNov.Services.Facade
{
    public sealed class PaymentFacade : PersianNovBaseFacade<Payment>, IPaymentFacade
    {
        public override bool Insert(Payment obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(obj);
        }
        public bool InsertPayment(Guid bookId, Guid customerId, long number)
        {
            base.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
            var book = new BookBO().Get(base.ConnectionHandler, bookId);
            var sum = book.Discount > 0 ? book.Price.Value - (book.Price.Value * book.Discount / 100) : book.Price.Value;
            try
            {
                var customerBook = new CustomerBook()
                {
                    BookId = bookId,
                    CustomerId = customerId,
                    VIP = true
                };
                if (!new CustomerBookBO().Insert(base.ConnectionHandler, customerBook))
                    throw new KnownException("خطا در ذخیره اطلاعات");
                var order = new Order()
                {
                    Number = number,
                    OrderDate = DateTime.Now.ShamsiDate(),
                    TotalAmount = book.Price.Value,
                    Discount = book.Discount,
                    Amount = sum,
                    Status = PaymentStatus.Success,
                    CustomerId = customerId,
                    BookId = bookId
                };
                if (!new OrderBO().Insert(base.ConnectionHandler, order))
                    throw new KnownException("خطا در ذخیره اطلاعات");
                var peyment = new Payment()
                {
                    Number = number,
                    OrderId = order.Id,
                    Amount = sum,
                    PaymentDate = DateTime.Now.ShamsiDate(),
                    PaymentStatus = PaymentStatus.Success,
                    PaymentType = PaymentType.Online,
                    PaymentRole = PaymentRole.Success,
                    AuthorId = book.AuthorId,
                    CustomerId = customerId
                };
                if (!new PaymentBO().Insert(base.ConnectionHandler, peyment))
                    throw new KnownException("خطا در ذخیره اطلاعات");
                var walletBo = new WalletBO();
                var amount = sum * book.Percent / 100;
                var wa = new Wallet()
                {
                    AuthorId = book.AuthorId,
                    Amount = amount,
                    Input = amount,
                    Number = number,
                    BookId = bookId
                };
                if (!walletBo.Insert(base.ConnectionHandler, wa))
                    throw new KnownException("خطا در ذخیره اطلاعات");

                base.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                base.ConnectionHandler.RollBack();
                throw new KnownException(ex.Message);
            }
        }
    }
}
