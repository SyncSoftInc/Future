using SyncSoft.Future.Logistics.MySql;

namespace SyncSoft.Future.Product.MySql
{

    public class ProductDB : MerchantExclusiveDB, IProductDB
    {
        public ProductDB(string connStrName) : base(connStrName)
        {
        }
    }
}
