using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Logistics.API.Inventory;
using SyncSoft.Future.Logistics.API.Warehouse;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseLogisticsApi(this CommonConfigurator configurator)
        {
            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<IInventoryApi, InventoryApi>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<IWarehouseApi, WarehouseApi>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
