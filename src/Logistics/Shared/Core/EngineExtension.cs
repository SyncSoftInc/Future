﻿using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Logistics.Domain.Warehouse;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseWarehouseCore(this CommonConfigurator configurator)
        {
            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<IWarehouseIdFactory, WarehouseIdFactory>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
