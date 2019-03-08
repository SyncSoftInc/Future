using SyncSoft.App.Redis;

namespace SyncSoft.Future.Redis
{
    public class MerchantSettingDB : RedisDB, IMerchantSettingDB
    {
        public MerchantSettingDB(string connStrName) : base(connStrName)
        {
        }
    }
}
