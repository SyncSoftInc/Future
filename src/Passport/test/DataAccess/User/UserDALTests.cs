using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.Enums.User;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.DataAccess.User;
using System;
using System.Threading.Tasks;

namespace DataAccessTest.User
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
            Status = UserStatusEnum.Active,
            Roles = 1,
            PermissionLevel = 5
        };

        [Test, Order(0)]
        public async Task InsertUser()
        {
            var dto = _userDto.DeepClone();

            var msgCode = await  _UserDAL.InsertUserAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(10)]
        public async Task UpdateUser()
        {
            var dto = _userDto.DeepClone();
            dto.FirstName = "updated first";
            dto.LastName = "updated last";
            dto.MiddleName = "updated middle";
            dto.Email = "updated email";
            dto.Status = UserStatusEnum.Inactive;
            dto.PermissionLevel = 0;

            var msgCode = await  _UserDAL.UpdateUserAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(100)]
        public async Task DeleteUser()
        {
            var msgCode = await  _UserDAL.DeleteUserAsync(_userDto.ID).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}
