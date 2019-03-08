using SyncSoft.App.DataAccess;
using SyncSoft.App.MySql;
using System.Data;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.MySql
{
    public abstract class LogisticsDAL : MySqlDAL
    {
        public LogisticsDAL(ISqlDatabase db) : base(db)
        {
        }

        protected Task<IDbConnection> CreateConn(string merchantId, bool openConnection = false)
            => DB.CreateConnectionAsync(o =>
            {
                o.OpenConnection = openConnection;
                o.AddParameter(LogisticsDB.Parameter_MerchantId, merchantId);
            });
    }
}
