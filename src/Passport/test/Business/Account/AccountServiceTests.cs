using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.Commands.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.Enums.Account;
using SyncSoft.Future.Passport.Domain.Account;
using System;
using System.Threading.Tasks;

namespace BusinessTest.Account
{
    public class AccountServiceTests
    {
        private static readonly Lazy<IAccountService> _lazyAccountService = ObjectContainer.LazyResolve<IAccountService>();
        private IAccountService _AccountService => _lazyAccountService.Value;

        private AccountDTO _accountDto = new AccountDTO
        {
            ID = new Guid("{2FA77635-63E4-493C-8930-BCC186AD0326}"),
            Username = "sa",
            Password = "Famous901",
            PasswordSalt = "ABCDEFG",
            LoginFailedCount = 0,
            LastLoginIP = "127.0.0.1",
            Status = AccountStatusEnum.Active,
            UpdatedOnUtc = DateTime.UtcNow,
            CreatedOnUtc = DateTime.UtcNow,
            LastLoginUtc = DateTime.UtcNow,
        };

        [Test, Order(0)]
        public async Task InsertAccount()
        {
            var cmd = new CreateAccountCommand
            {
                ID = _accountDto.ID,
                Username = _accountDto.Username,
                Password = _accountDto.Password,
                Status = _accountDto.Status,
            };

            var msgCode = await _AccountService.CreateAsync(cmd).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(1)]
        public async Task DeleteAccount()
        {
            var dto = await _AccountService.VerifyUsernamePasswordAsync(_accountDto.Username, _accountDto.Password).ConfigureAwait(false);
            Assert.IsNotNull(dto);

            var msgCode = await _AccountService.DeleteAsync(new DeleteAccountCommand { ID = dto.ID }).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}
