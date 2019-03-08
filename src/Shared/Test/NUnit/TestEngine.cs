using SyncSoft.App;
using SyncSoft.App.EngineConfigs;
using SyncSoft.App.Redis.Confgiguration;

namespace SyncSoft.Future
{
    public static class TestEngine
    {
        public static CommonConfigurator Init()
        {
            return Engine.Init()
                .UseSimpleInjector()
                .UseSerilogLoggerAppQuickSettings(options =>
                {
                    options.ConfigAppDefaultComponentsOptions = d =>
                    {
                        d.ConnectionStringProviderType = typeof(RedisConnectionStringProvider);
                    };
                })
                .UseJsonNet()
                .AsUnitTestApiClient();
        }
    }
}
