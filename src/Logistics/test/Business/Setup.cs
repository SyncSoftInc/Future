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
                .UseWarehouseDomain()
                .UseWarehouseRedis()
                .UseWarehouseMySql()
                .Start();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
