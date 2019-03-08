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
                .UseWarehouseRedis()
                .UseWarehouseAPI()
                .UseWarehouseCore()
                .Start();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
