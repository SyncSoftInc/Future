namespace SyncSoft.Future.Logistics.MySql
{

    public class LogisticsDB : MerchantExclusiveDB, ILogisticsDB
    {
        public LogisticsDB(string connStrName) : base(connStrName)
        {
        }
    }
}
