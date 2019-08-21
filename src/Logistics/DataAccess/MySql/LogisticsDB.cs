using SyncSoft.App.MySql;

namespace SyncSoft.Future.Logistics.MySql
{

    public class LogisticsDB : MySqlDatabase, ILogisticsDB
    {
        public LogisticsDB(string connStrName) : base(connStrName)
        {
        }
    }
}
