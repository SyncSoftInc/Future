using NUnit.Framework;
using SyncSoft.App;
using SyncSoft.ECP.Securities;

namespace SyncSoft.Future.Passport.BusinessTest
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
                .RegisterComponent<IPasswordEncryptor, Sha256PasswordEncryptor>(App.Components.LifeCycleEnum.Singleton)
                .Start();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
