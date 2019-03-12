using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.Redis.Inventory;

namespace SyncSoft.App
{
    public static class EngineExtensions
    {
        public static CommonConfigurator UseLogisticsRedis(this CommonConfigurator configurator)
        {
            configurator.UseFutureRedis();

            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<IInventoryQueryDAL, InventoryDAL>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<IInventoryDB>(() => new InventoryDB(CONSTANTS.CONNECTION_STRINGS.REDIS_DEFAULT), LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
