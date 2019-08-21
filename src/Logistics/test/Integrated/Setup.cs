using NUnit.Framework;
using SyncSoft.App;

namespace SyncSoft.Future.Logistics.IntegratedTest
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
                .UseLogisticsCore();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
