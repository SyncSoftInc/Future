using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Logistics.DataFacade.Inventory;
using SyncSoft.Future.Logistics.DataFacade.Warehouse;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseWarehouseDF(this CommonConfigurator configurator)
        {
            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<IInventoryDF, InventoryDF>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<IWarehouseDF, WarehouseDF>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
