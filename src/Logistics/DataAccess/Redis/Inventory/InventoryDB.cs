using SyncSoft.App.Redis;

namespace SyncSoft.Future.Logistics.Redis.Inventory
{
    public class InventoryDB : RedisDB, IInventoryDB
    {
        public InventoryDB(string connStrName) : base(connStrName)
        { }
    }
}
