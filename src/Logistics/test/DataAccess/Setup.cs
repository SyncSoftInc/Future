using NUnit.Framework;
using SyncSoft.App;

namespace SyncSoft.Future.Warehouse.DataAccessTest
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public static void Startup()
        {
            TestEngine.Init()
                .UseLogisticsRedis()
                .UseLogisticsMySql()
                .Start();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
