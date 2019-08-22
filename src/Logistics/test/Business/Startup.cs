using NUnit.Framework;
using SyncSoft.App;
using SyncSoft.Future;

namespace BusinessTest
{
    [SetUpFixture]
    public class Startup
    {
        [OneTimeSetUp]
        public static void Start()
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
