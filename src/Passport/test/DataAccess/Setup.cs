using NUnit.Framework;
using SyncSoft.App;
using SyncSoft.Future;

namespace DataAccessTest
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public static void Startup()
        {
            TestEngine.Init()
                .UseFutureRedis()
                .UsePassportMySql()
                .UseJsonConfiguration()
                .Start();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
