using NUnit.Framework;
using SyncSoft.App;
using SyncSoft.App.Components;
using SyncSoft.ECP.Securities;
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
                .UseFutureRedis()
                .UsePassportDomain()
                .UsePassportMySql()
                .RegisterComponent<IPasswordEncryptor, Sha256PasswordEncryptor>(LifeCycleEnum.Singleton)
                .UseJsonConfiguration()
                .Start();
        }

        //[OneTimeTearDown]
        //public async Task TearDown()
        //{
        //}
    }
}
