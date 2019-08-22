using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.Future.Passport.API.Account;
using System;
using System.Threading.Tasks;

namespace IntegratedTest.Account
{
    public class AccountApiTest
    {
        private static readonly Lazy<IAccountApi> _lazyAccountApi = ObjectContainer.LazyResolve<IAccountApi>();
        private IAccountApi _AccountApi => _lazyAccountApi.Value;

        private readonly AccountDTO _account = new AccountDTO
        {
            Username = "sa",
            Password = "Famous901"
        };

        [Test, Order(0)]
        public async Task CreateAccount()
        {
            var hr = await _AccountApi.CreateAccountAsync(new
            {
                Username = "sa",
                Password = "Famous901"
            }).ConfigureAwait(false);

            var msgCode = await hr.GetMsgCodeAsync().ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }


        [Test, Order(1)]
        public async Task DeleteAccount()
        {
            var accountHr = await _AccountApi.VerifyUsernamePasswordAsync(_account).ConfigureAwait(false);
            var dto = await accountHr.GetResultAsync().ConfigureAwait(false);
            Assert.IsNotNull(dto);

            var hr = await _AccountApi.DeleteAccountAsync(new
            {
                ID = dto.ID
            }).ConfigureAwait(false);

            var msgCode = await hr.GetMsgCodeAsync().ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}
