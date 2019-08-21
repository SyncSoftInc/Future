using SyncSoft.App.MySql;
using SyncSoft.Future.Logistics.DataAccess;

namespace SyncSoft.Future.Logistics.MySql
{

    public class LogisticsDB : MySqlDatabase, ILogisticsDB
    {
        public LogisticsDB(string connStrName) : base(connStrName)
        {
        }
    }
}
