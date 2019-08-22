using SyncSoft.App;
using SyncSoft.App.EngineConfigs;
using SyncSoft.App.Redis.Messaging;

namespace SyncSoft.Future
{
    public static class TestEngine
    {
        public static CommonConfigurator Init()
        {
            return Engine.Init()
                .UseSimpleInjector()
                .UseAppDefaultComponents(options =>
                {
                    options.MsgResultStoreType = typeof(RedisMsgResultStore);
                })
                .UseJsonNet()
                .UseSerilog()
                .UseECPRedis()
                .UseUnitTestApiClient();
        }
    }
}
