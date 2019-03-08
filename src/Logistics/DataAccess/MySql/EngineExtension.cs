using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Warehouse;
using SyncSoft.Future.Logistics.MySql;
using SyncSoft.Future.Logistics.MySql.Inventory;
using SyncSoft.Future.Logistics.MySql.Warehouse;
using System;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseWarehouseMySql(this CommonConfigurator configurator
            , Action<LogisticsMySqlOptions> configOptions = null
            , LogisticsMySqlOptions options = null)
        {
            options = options ?? new LogisticsMySqlOptions();
            configOptions?.Invoke(options);

            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<ILogisticsDB>(() => new LogisticsDB(options.ConnStrName), LifeCycleEnum.Singleton);

                    ObjectContainer.Register<IWarehouseDAL, WarehouseDAL>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<IInventoryMasterDAL, InventoryDAL>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
