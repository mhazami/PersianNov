using PersianNov.DataStructure;
using Radyn.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersianNov.Services.Interface
{
    public interface ICustomerFacade : IBaseFacade<Customer>
    {
        Task<Customer> Login(string username, string password);
    }
}
