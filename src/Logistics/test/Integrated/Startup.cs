using NUnit.Framework;
using SyncSoft.App;
using SyncSoft.Future;

namespace IntegratedTest
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public static void Startup()
        {
            TestEngine.Init()
                .UseLogisticsRedis()
                .UseLogisticsApi()
                .UseLogisticsCore()
                .UseJsonConfiguration()
                .Start();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
