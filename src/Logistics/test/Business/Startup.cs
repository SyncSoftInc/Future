using NUnit.Framework;
using SyncSoft.App;
using SyncSoft.Future;

namespace BusinessTest
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public static void Startup()
        {
            TestEngine.Init()
                .UseLogisticsDomain()
                .UseLogisticsRedis()
                .UseLogisticsMySql()
                .UseJsonConfiguration()
                .Start();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
