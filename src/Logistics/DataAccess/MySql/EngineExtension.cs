using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Logistics.DataAccess;
using SyncSoft.Future.Logistics.MySql;
using System;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseLogisticsMySql(this CommonConfigurator configurator
            , Action<LogisticsMySqlOptions> configOptions = null
            , LogisticsMySqlOptions options = null)
        {
            options = options ?? new LogisticsMySqlOptions();
            configOptions?.Invoke(options);

            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    //ObjectContainer.Register<ILogisticsDB>(() => new LogisticsDB(options.ConnStrName), LifeCycleEnum.Singleton);

                    //ObjectContainer.Register<IWarehouseDAL, WarehouseDAL>(LifeCycleEnum.Singleton);
                    //ObjectContainer.Register<IInventoryMasterDAL, InventoryDAL>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<ILogisticsMasterDALFactory>(() => new LogisticsMasterDALFactory(options.ConnStrName), LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
