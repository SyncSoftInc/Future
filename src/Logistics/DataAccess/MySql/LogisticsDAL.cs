using SyncSoft.App.DataAccess;
using SyncSoft.App.MySql;

namespace SyncSoft.Future.Logistics.MySql
{
    public abstract class LogisticsDAL : MySqlDAL
    {
        public LogisticsDAL(ISqlDatabase db) : base(db)
        {
        }
    }
}
