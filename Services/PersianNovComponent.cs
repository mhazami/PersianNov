using PersianNov.Services.Facade;
using PersianNov.Services.Interface;


namespace PersianNov.Services
{
    public class PersianNovComponent
    {
        private PersianNovComponent()
        {

        }

        private static PersianNovComponent _instance;
        public static PersianNovComponent Instance
        {
            get { return _instance ?? (_instance = new PersianNovComponent()); }
        }
        public static string ConnectionString { get; set; }
        public IAuthorFacade AuthorFacade
        {
            get
            {
                return new AuthorFacade();
            }
        }

        public IUserFacade UserFacade
        {
            get
            {
                return new UserFacade();
            }
        }


        public IAuthorBookFacade AuthorBookFacade
        {
            get
            {
                return new AuthorBookFacade();
            }
        }

        public IBookFacade BookFacade
        {
            get
            {
                return new BookFacade();
            }
        }

        public IBookPartFacade BookPartFacade
        {
            get
            {
                return new BookPartFacade();
            }
        }

        public ICustomerBookFacade CustomerBookFacade
        {
            get
            {
                return new CustomerBookFacade();
            }
        }

        public ICustomerFacade CustomerFacade
        {
            get
            {
                return new CustomerFacade();
            }
        }


        public IFileFacade FileFacade
        {
            get
            {
                return new FileFacade();
            }
        }

        public IOrderFacade OrderFacade
        {
            get
            {
                return new OrderFacade();
            }
        }

        public IPaymentFacade PaymentFacade
        {
            get
            {
                return new PaymentFacade();
            }
        }

        public IPublisherBookFacade PublisherBookFacade
        {
            get
            {
                return new PublisherBookFacade();
            }
        }

        public IPublisherFacade PublisherFacade
        {
            get
            {
                return new PublisherFacade();
            }
        }

        public ITaskMoneyFacade TaskMoneyFacade
        {
            get
            {
                return new TaskMoneyFacade();
            }
        }

        public IWalletFacade WalletFacade
        {
            get
            {
                return new WalletFacade();
            }
        }


    }
}
