using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.Enums.Account;
using SyncSoft.ECP.Enums.User;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.Command.User;
using SyncSoft.Future.Passport.Domain.User;
using System;
using System.Threading.Tasks;

namespace BusinessTest.User
{
    public class UserServiceTests
    {
        private static readonly Lazy<IUserService> _lazyUserService = ObjectContainer.LazyResolve<IUserService>();
        private IUserService _UserService => _lazyUserService.Value;

        private UserDTO _userDto = new UserDTO
        {
            ID = new Guid("{2FA77635-63E4-493C-8930-BCC186AD0326}"),
            FirstName = "first",
            LastName = "last",
            MiddleName = "middle",
            Email = "test@syncsoftinc.com",
            Status = UserStatusEnum.Active,
            Roles = (int)UserRoleEnum.Admin,
            PermissionLevel = 5
        };

        private AccountDTO _accountDto = new AccountDTO
        {
            ID = new Guid("{2FA77635-63E4-493C-8930-BCC186AD0326}"),
            Username = "unittest",
            Password = "Famous901",
            Status = AccountStatusEnum.Active,
            CreatedOnUtc = DateTime.UtcNow,
        };

        [Test, Order(0)]
        public async Task InsertUser()
        {
            //// 建立SA用户用，去掉注释
            //_userDto.ID = new Guid("{00000000-0000-0000-0000-000000000001}");
            //_userDto.FirstName = "Super";
            //_userDto.LastName = "Admin";
            //_userDto.MiddleName = null;
            //_userDto.Email = "sa@syncsoftinc.com";
            //_accountDto.Username = "sa";

            var cmd = new CreateUserCommand
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

            var msgCode = await _UserService.CreateUserAsync(cmd).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(50)]
        public async Task UserSaveProfile()
        {
            var cmd = new UserSaveProfileCommand
            {
                ID = _userDto.ID,
                OldPassword = _accountDto.Password,
                Password = _accountDto.Password + "_UPDATE",
                FirstName = _userDto.FirstName + "_UPDATE",
                MiddleName = _userDto.MiddleName + "_UPDATE",
                LastName = _userDto.LastName + "_UPDATE",
                Email = _userDto.Email + "_UPDATE",
            };

            var msgCode = await _UserService.UserSaveProfileAsync(cmd).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(75)]
        public async Task UpdateUser()
        {
            var cmd = new UpdateUserCommand
            {
                ID = _userDto.ID,
                Username = _accountDto.Username + "_UPDATE",
                Password = _accountDto.Password + "_UPDATE",
                FirstName = _userDto.FirstName + "_UPDATE",
                MiddleName = _userDto.MiddleName + "_UPDATE",
                LastName = _userDto.LastName + "_UPDATE",
                Email = _userDto.Email + "_UPDATE",
                Status = (int)UserStatusEnum.Inactive,
                Roles = 5,
                PermissionLevel = 1
            };

            var msgCode = await _UserService.UpdateUserAsync(cmd).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(100)]
        public async Task DeleteUser()
        {
            var cmd = new DeleteUserCommand { ID = _userDto.ID, };
            var msgCode = await _UserService.DeleteUserAsync(cmd).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}
