using PersianNov.DataStructure;
using PersianNov.Services.BO;
using PersianNov.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.Services.Facade
{
    public sealed class WalletFacade : PersianNovBaseFacade<Wallet>, IWalletFacade
    {
        public override bool Insert(Wallet obj)
        {
            obj.Id = Guid.NewGuid();
            return base.Insert(obj);
        }
        public decimal GetAuthorAmount(Guid authorId)
        {
            var input = new WalletBO().Sum(ConnectionHandler, x => x.Input, x => x.AuthorId == authorId);
            var output = new WalletBO().Sum(ConnectionHandler, x => x.Output, x => x.AuthorId == authorId);
            var dif = input - output;
            return dif.HasValue ? dif.Value : 0;
        }
    }
}
