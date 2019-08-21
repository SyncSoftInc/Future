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
        public void InsertAccount()
        {
            var dto = _accountDto.DeepClone();

            var msgCode = _AccountDAL.InsertAccountAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(10)]
        public void UpdateAccountStatus()
        {
            var dto = _accountDto.DeepClone();
            dto.Status = AccountStatusEnum.Inactive;

            var msgCode = _AccountDAL.UpdateAccountStatusAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(100)]
        public void UpdateLoginInfo()
        {
            var dto = _accountDto.DeepClone();
            dto.LoginFailedCount = 3;
            dto.LastLoginIP = "updated LastLoginIP";
            dto.UpdatedOnUtc = DateTime.UtcNow;
            dto.LastLoginUtc = DateTime.UtcNow;

            var msgCode = _AccountDAL.UpdateLoginInfoAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(1000)]
        public void UpdatePassword()
        {
            var dto = _accountDto.DeepClone();
            dto.Password = "updated password";
            dto.PasswordSalt = "updated passwordsalt";
            dto.LoginFailedCount = 0;

            var msgCode = _AccountDAL.UpdatePasswordAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(10000)]
        public void UpdateUsername()
        {
            var dto = _accountDto.DeepClone();
            dto.Username = "Username";

            var msgCode = _AccountDAL.UpdateUsernameAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(100000)]
        public void IsUsernameExist()
        {
            var rs = _AccountDAL.IsUsernameExistAsync(_accountDto.Username).Execute();
            Assert.IsTrue(rs);
        }

        [Test, Order(1000000)]
        public void GetAccountByID()
        {
            var rs = _AccountDAL.GetAccountAsync(_accountDto.ID).Execute();
            Assert.IsTrue(rs.IsNotNull());
        }

        [Test, Order(10000000)]
        public void GetAccountByUsername()
        {
            var rs = _AccountDAL.GetAccountAsync(_accountDto.Username).Execute();
            Assert.IsTrue(rs.IsNotNull());
        }

        [Test, Order(100000000)]
        public void DeleteAccount()
        {
            var msgCode = _AccountDAL.DeleteAccountAsync(_accountDto.ID).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}
