using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.Commands.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.Enums.Account;
using SyncSoft.Future.Passport.Domain.Account;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.BusinessTest.Account
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

        [Test]
        public void InsertAccount()
        {
            var cmd = new CreateAccountCommand
            {
                ID = _accountDto.ID,
                Username = _accountDto.Username,
                Password = _accountDto.Password,
                Status = _accountDto.Status,
            };

            var msgCode = _AccountService.CreateAsync(cmd).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test]
        public void VerifyUsernamePassword()
        {
            var dto = _AccountService.VerifyUsernamePasswordAsync(_accountDto.Username, _accountDto.Password).Execute();
            Assert.IsNotNull(dto);
        }
    }
}
