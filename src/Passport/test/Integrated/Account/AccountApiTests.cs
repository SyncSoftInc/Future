using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.Future.Passport.API.Account;
using System;
using System.Threading.Tasks;

namespace IntegratedTest.Account
{
    public class AccountApiTest
    {
        private static readonly Lazy<IAccountApi> _lazyAccountApi = ObjectContainer.LazyResolve<IAccountApi>();
        private IAccountApi _AccountApi => _lazyAccountApi.Value;

        [Test]
        public void VerifyUsernamePassword()
        {
            var rs = _AccountApi.VerifyUsernamePasswordAsync(new { Username = "sa", Password = "Famous901" }).ResultForTest();
            Assert.IsNotNull(rs);
        }

        [Test]
        public void CreateAccount()
        {
            var msgCode = _AccountApi.CreateAccountAsync(new
            {
                Username = "sa",
                Password = "Famous901"
            }).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }
    }
}
