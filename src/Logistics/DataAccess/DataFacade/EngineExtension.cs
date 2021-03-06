﻿using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Warehouse;
using SyncSoft.Future.Logistics.DataFacade.Inventory;
using SyncSoft.Future.Logistics.DataFacade.Warehouse;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseLogisticsDF(this CommonConfigurator configurator)
        {
            Engine.PreventDuplicateRegistration(nameof(UseLogisticsDF));

            configurator.Engine.Starting += (o, e) =>
            {
                ObjectContainer.Register<IInventoryDF, InventoryDF>(LifeCycleEnum.Singleton);
                ObjectContainer.Register<IWarehouseDF, WarehouseDF>(LifeCycleEnum.Singleton);
            };

            return configurator;
        }
    }
}
