using PersianNov.DataStructure;
using Radyn.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.Services.Interface
{
    public interface IPaymentFacade : IBaseFacade<Payment>
    {
        bool InsertPayment(Guid bookId, Guid customerId, long number);
    }
}
