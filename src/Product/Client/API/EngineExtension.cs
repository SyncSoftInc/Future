using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseProductApi(this CommonConfigurator configurator)
        {
            {
                if (!Engine.IsStarted)
                {
                    configurator.Engine.Starting += (o, e) =>
                    {
                    };
                }

                return configurator;
            }
        }
    }
}
