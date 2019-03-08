using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.Enums.Account;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.DataAccessTest.Account
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
        public void DeleteAccount()
        {
            var msgCode = _AccountDAL.DeleteAccountAsync(_accountDto.ID).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}
