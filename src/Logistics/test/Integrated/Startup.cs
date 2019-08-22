using NUnit.Framework;
using SyncSoft.App;
using SyncSoft.Future;

namespace IntegratedTest
{
    [SetUpFixture]
    public class Startup
    {
        [OneTimeSetUp]
        public static void Start()
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
