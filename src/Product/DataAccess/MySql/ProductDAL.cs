using SyncSoft.App.DataAccess;
using SyncSoft.App.MySql;
using System.Data;
using System.Threading.Tasks;

namespace SyncSoft.Future.Product.MySql
{
    public abstract class ProductDAL : MySqlDAL
    {
        public ProductDAL(ISqlDatabase db) : base(db)
        {
        }

        protected Task<IDbConnection> CreateConn(string merchantId, bool openConnection = false)
            => DB.CreateConnectionAsync(o =>
            {
                o.OpenConnection = openConnection;
                o.AddParameter(ProductDB.Parameter_MerchantId, merchantId);
            });
    }
}
