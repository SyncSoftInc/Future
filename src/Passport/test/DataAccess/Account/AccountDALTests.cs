using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.Enums.Account;
using System;
using System.Threading.Tasks;

namespace DataAccessTest.Account
{
    public class AccountDALTests
    {
        private static readonly Lazy<IAccountDAL> _lazyAccountDAL = ObjectContainer.LazyResolve<IAccountDAL>();
        private IAccountDAL _AccountDAL => _lazyAccountDAL.Value;

        private AccountDTO _accountDto = new AccountDTO
        {
            ID = new Guid("ec35e329dac34842af18f0028763a059"),
            Username = "Username",
            Password = "Password",
            PasswordSalt = "PasswordSalt",
            LoginFailedCount = 0,
            LastLoginIP = "LastLoginIP",
            Status = AccountStatusEnum.Active,
            UpdatedOnUtc = DateTime.UtcNow,
            CreatedOnUtc = DateTime.UtcNow,
            LastLoginUtc = DateTime.UtcNow,
        };

        [Test, Order(0)]
        public async Task InsertAccount()
        {
            var dto = _accountDto.DeepClone();

            var msgCode = await _AccountDAL.InsertAccountAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(10)]
        public async Task UpdateAccountStatus()
        {
            var dto = _accountDto.DeepClone();
            dto.Status = AccountStatusEnum.Inactive;

            var msgCode = await _AccountDAL.UpdateAccountStatusAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(100)]
        public async Task UpdateLoginInfo()
        {
            var dto = _accountDto.DeepClone();
            dto.LoginFailedCount = 3;
            dto.LastLoginIP = "updated LastLoginIP";
            dto.UpdatedOnUtc = DateTime.UtcNow;
            dto.LastLoginUtc = DateTime.UtcNow;

            var msgCode = await _AccountDAL.UpdateLoginInfoAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(1000)]
        public async Task UpdatePassword()
        {
            var dto = _accountDto.DeepClone();
            dto.Password = "updated password";
            dto.PasswordSalt = "updated passwordsalt";
            dto.LoginFailedCount = 0;

            var msgCode = await _AccountDAL.UpdatePasswordAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(10000)]
        public async Task UpdateUsername()
        {
            var dto = _accountDto.DeepClone();
            dto.Username = "Username";

            var msgCode = await _AccountDAL.UpdateUsernameAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(100000)]
        public async Task IsUsernameExist()
        {
            var rs = await _AccountDAL.IsUsernameExistAsync(_accountDto.Username).ConfigureAwait(false);
            Assert.IsTrue(rs);
        }

        [Test, Order(1000000)]
        public async Task GetAccountByID()
        {
            var rs = await _AccountDAL.GetAccountAsync(_accountDto.ID).ConfigureAwait(false);
            Assert.IsTrue(rs.IsNotNull());
        }

        [Test, Order(10000000)]
        public async Task GetAccountByUsername()
        {
            var rs = await _AccountDAL.GetAccountAsync(_accountDto.Username).ConfigureAwait(false);
            Assert.IsTrue(rs.IsNotNull());
        }

        [Test, Order(100000000)]
        public async Task DeleteAccount()
        {
            var msgCode = await _AccountDAL.DeleteAccountAsync(_accountDto.ID).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}
