using NUnit.Framework;
using SyncSoft.App;

namespace SyncSoft.Future.Logistics.BusinessTest
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
                .UseLogisticsMySql();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
