using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.API.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.IntegratedTest.User
{
    public class UserApiTests
    {
        private static readonly Lazy<IUserApi> _lazyUserApi = ObjectContainer.LazyResolve<IUserApi>();
        private IUserApi _UserApi => _lazyUserApi.Value;

        private UserDTO _userDto = new UserDTO
        {
            ID = new Guid("{2FA77635-63E4-493C-8930-BCC186AD0326}"),
            FirstName = "first",
            LastName = "last",
            MiddleName = "middle",
            Email = "test@syncsoftinc.co",
            Status = ECP.Enums.User.UserStatusEnum.Active,
            Roles = 1,
            PermissionLevel = 5
        };

        private AccountDTO _accountDto = new AccountDTO
        {
            ID = new Guid("{2FA77635-63E4-493C-8930-BCC186AD0326}"),
            Username = "sa",
            Password = "Famous901",
            PasswordSalt = "ABCDEFG",
            LoginFailedCount = 0,
            LastLoginIP = "127.0.0.1",
            Status = ECP.Enums.Account.AccountStatusEnum.Active,
            UpdatedOnUtc = DateTime.UtcNow,
            CreatedOnUtc = DateTime.UtcNow,
            LastLoginUtc = DateTime.UtcNow,
        };

        [Test, Order(0)]
        public void CreateUser()
        {
            var cmd = new
            {
                ID = _userDto.ID,
                Username = _accountDto.Username,
                Password = _accountDto.Password,
                FirstName = _userDto.FirstName,
                MiddleName = _userDto.MiddleName,
                LastName = _userDto.LastName,
                Email = _userDto.Email,
                Status = (int)_userDto.Status,
                Roles = _userDto.Roles,
                PermissionLevel = _userDto.PermissionLevel
            };

            var msgCode = _UserApi.CreateUserAsync(cmd).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(25)]
        public void UpdateUser()
        {
            var cmd = new
            {
                ID = _userDto.ID,
                Username = _accountDto.Username + "_UPDATE",
                Password = _accountDto.Password + "_UPDATE",
                FirstName = _userDto.FirstName + "_UPDATE",
                MiddleName = _userDto.MiddleName + "_UPDATE",
                LastName = _userDto.LastName + "_UPDATE",
                Email = _userDto.Email + "_UPDATE",
                Status = (int)ECP.Enums.User.UserStatusEnum.Inactive,
                Roles = 5,
                PermissionLevel = 1
            };

            var msgCode = _UserApi.UpdateUserAsync(cmd).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(50)]
        public void UserSaveProfile()
        {
            var cmd = new
            {
                ID = _userDto.ID,
                OldPassword = _accountDto.Password,
                Password = _accountDto.Password + "_UPDATE",
                FirstName = _userDto.FirstName + "_UPDATE",
                MiddleName = _userDto.MiddleName + "_UPDATE",
                LastName = _userDto.LastName + "_UPDATE",
                Email = _userDto.Email + "_UPDATE",
            };

            var msgCode = _UserApi.UserSaveProfileAsync(cmd).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        //[Test, Order(75)]
        //public void DeleteUser()
        //{
        //    var msgCode = _UserApi.DeleteUserAsync(_userDto.ID).ResultForTest();
        //    Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        //}
    }
}
