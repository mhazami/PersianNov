﻿using PersianNov.DataStructure;
using Radyn.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.Services.Interface
{
    public interface IWalletFacade : IBaseFacade<Wallet>
    {
        decimal GetAuthorAmount(Guid authorId);
    }
}
