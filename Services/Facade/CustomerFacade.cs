using PersianNov.DataStructure;
using PersianNov.Services.BO;
using PersianNov.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersianNov.Services.Facade
{
    public sealed class CustomerFacade : PersianNovBaseFacade<Customer>, ICustomerFacade
    {
        public async Task<Customer> Login(string username, string password)
        {
            return await new CustomerBO().Login(base.ConnectionHandler, username, password);
        }
    }
}
