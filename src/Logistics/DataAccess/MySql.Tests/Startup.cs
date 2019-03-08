using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyncSoft.App;

namespace SyncSoft.Show.Warehouse.MySql.Tests
{
    [TestClass]
    public class Startup
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext)
        {
            Engine.Init()
                .UseSimpleInjector()
                .UseDiagnosticLoggerAppQuickSettings(o =>
                {
                    o.ConfigAppDefaultComponentsOptions = a =>
                    {
                        a.ConnectionStringProviderType = typeof(SyncSoft.App.Redis.Confgiguration.RedisConnectionStringProvider);
                    };
                })
                .UseJsonNet()
                .UseWarehouseMySql()
                .UseWarehouseRedis()
                .Start();
        }
    }
}
