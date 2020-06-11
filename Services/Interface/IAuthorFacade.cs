using PersianNov.DataStructure;
using Radyn.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersianNov.Services.Interface
{
    public interface IAuthorFacade : IBaseFacade<Author>
    {
        Author Login(string username, string password);
    }
}
