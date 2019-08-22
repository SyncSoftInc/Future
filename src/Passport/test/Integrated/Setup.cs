using NUnit.Framework;
using SyncSoft.App;
using SyncSoft.Future;

namespace IntegratedTest
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public static void Startup()
        {
            TestEngine.Init()
                .UseFutureRedis()
                .UsePassportAPI()
                .UseJsonConfiguration()
                .Start();
        }

        //[OneTimeTearDown]
        //public async Task TearDown()
        //{
        //}
    }
}
