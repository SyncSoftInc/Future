using NUnit.Framework;
using SyncSoft.App;
using SyncSoft.Future;

namespace DataAccessTest
{
    [SetUpFixture]
    public class Startup
    {
        [OneTimeSetUp]
        public static void Start()
        {
            TestEngine.Init()
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
