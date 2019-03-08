using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Logistics.Domain.Inventory;
using SyncSoft.Future.Logistics.Domain.Warehouse;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseWarehouseDomain(this CommonConfigurator configurator)
        {
            configurator.UseWarehouseCore();

            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<IInventoryService, InventoryService>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<IWarehouseService, WarehouseService>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
