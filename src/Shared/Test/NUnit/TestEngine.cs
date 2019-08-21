using SyncSoft.App;
using SyncSoft.App.EngineConfigs;

namespace SyncSoft.Future
{
    public static class TestEngine
    {
        public static CommonConfigurator Init()
        {
            return Engine.Init()
                .UseSimpleInjector()
                .UseAppDefaultComponents()
                .UseJsonNet()
                .UseUnitTestApiClient();
        }
    }
}
