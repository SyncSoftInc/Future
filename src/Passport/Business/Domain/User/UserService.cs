using SyncSoft.App.Components;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.Future.Passport.Command.User;
using SyncSoft.Future.Passport.DAL.User;
using SyncSoft.Future.Passport.Domain.User.CreateUser;
using SyncSoft.Future.Passport.Domain.User.DeleteUser;
using SyncSoft.Future.Passport.Domain.User.UpdateUser;
using SyncSoft.Future.Passport.Domain.User.UserSaveProfile;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User
{
    public class UserService : IUserService
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IUserDAL> _lazyUserDAL = ObjectContainer.LazyResolve<IUserDAL>();
        private IUserDAL _UserDAL => _lazyUserDAL.Value;

        private static readonly Lazy<IAccountDAL> _lazyAccountDAL = ObjectContainer.LazyResolve<IAccountDAL>();
        private IAccountDAL _AccountDAL => _lazyAccountDAL.Value;


        #endregion
        // *******************************************************************************************************************************
        #region -  CreateUserAsync  -

        public async Task<string> CreateUserAsync(CreateUserCommand cmd)
        {
            // 检查用户名是否存在
            var exists = await _AccountDAL.IsUsernameExistAsync(cmd.Username).ConfigureAwait(false);
            if (exists) return MsgCodes.PASS_0000000001;
            // ^^^^^^^^^^

            // 开始创建事务
            var tran = new CreateUserTransaction(cmd);
            bool success = await tran.RunAsync().ConfigureAwait(false);
            return success ? MsgCodes.SUCCESS : MsgCodes.PASS_0000000002;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UpdateUserAsync  -

        public async Task<string> UpdateUserAsync(UpdateUserCommand cmd)
        {
            var tran = new UpdateUserTransaction(cmd);
            bool success = await tran.RunAsync().ConfigureAwait(false);
            return success ? MsgCodes.SUCCESS : MsgCodes.PASS_0000000002;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UserSaveProfileAsync  -

        public async Task<string> UserSaveProfileAsync(UserSaveProfileCommand cmd)
        {
            var tran = new UserSaveProfileTransaction(cmd);
            bool success = await tran.RunAsync().ConfigureAwait(false);
            return success ? MsgCodes.SUCCESS : MsgCodes.PASS_0000000002;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  DeleteUserAsync  -

        public async Task<string> DeleteUserAsync(DeleteUserCommand cmd)
        {
            var tran = new DeleteUserTransaction(cmd);
            bool success = await tran.RunAsync().ConfigureAwait(false);
            return success ? MsgCodes.SUCCESS : MsgCodes.PASS_0000000002;
        }

        #endregion
    }
}
