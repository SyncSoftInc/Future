using SyncSoft.App.EngineConfigs;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UseProductRedis(this CommonConfigurator configurator)
        {
            configurator.UseFutureRedis();

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
