using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.DTOs.Users;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.MySql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.DataAccessTest.User
{
    public class UserDALTests
    {
        private static readonly Lazy<IUserDAL> _lazyUserDAL = ObjectContainer.LazyResolve<IUserDAL>();
        private IUserDAL _UserDAL => _lazyUserDAL.Value;

        private UserDTO _userDto = new UserDTO
        {
            ID = new Guid("ec35e329dac34842af18f0028763a059"),
            FirstName = "first",
            LastName = "last",
            MiddleName = "middle",
            Email = "email",
            Status = ECP.Enums.User.UserStatusEnum.Active,
            Roles = 1,
            PermissionLevel = 5
        };

        [Test, Order(0)]
        public void InsertUser()
        {
            var dto = _userDto.DeepClone();

            var msgCode = _UserDAL.InsertUserAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(10)]
        public void UpdateUser()
        {
            var dto = _userDto.DeepClone();
            dto.FirstName = "updated first";
            dto.LastName = "updated last";
            dto.MiddleName = "updated middle";
            dto.Email = "updated email";
            dto.Status = ECP.Enums.User.UserStatusEnum.Inactive;
            dto.PermissionLevel = 0;

            var msgCode = _UserDAL.UpdateUserAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(100)]
        public void DeleteUser()
        {
            var msgCode = _UserDAL.DeleteUserAsync(_userDto.ID).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}
