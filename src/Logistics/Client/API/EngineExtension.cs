using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Logistics.API;
using SyncSoft.Future.Logistics.API.Warehouse;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseWarehouseAPI(this CommonConfigurator configurator)
        {
            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<IWarehouseApi, WarehouseApi>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
