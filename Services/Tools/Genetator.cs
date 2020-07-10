using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PersianNov.Services.Tools
{
    public sealed class Genetator<T> : PersianNovBaseFacade<T> where T : class
    {
        public long NumberGenerator(Expression<Func<T, long>> query)
        {
            var bigest = this.Max(query);
            if (bigest != 0)
                bigest++;
            else
                bigest = 1451045;
            return bigest;
        }
    }
}
