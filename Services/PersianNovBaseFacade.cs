using Radyn.Framework;
using Radyn.Framework.DbHelper;



namespace PersianNov.Services
{
    public abstract class PersianNovBaseFacade<T> : BaseFacade<T> where T : class
    {

        protected PersianNovBaseFacade()
            : base(new PersianNovConnectionHandler(), false)
        {

        }
        protected PersianNovBaseFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {

        }


    }
    public class PersianNovConnectionHandler : ConnectionHandler
    {
        public PersianNovConnectionHandler()
        {
            base.ConnectionString = "Data Source=(Local);Initial Catalog=PersianNov;persist security info=True;User ID=sa;Password=123;Max Pool Size=10000;MultipleActiveResultSets=True;";
        }

    }
}
