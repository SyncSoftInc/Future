using SyncSoft.App.Components;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.Future.Passport.Command.User;
using SyncSoft.Future.Passport.DAL.User;
using SyncSoft.Future.Passport.Domain.User.CreateUser;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User
{
    public class UserService : IUserService
    {
        private static readonly Lazy<IUserDAL> _lazyUserDAL = ObjectContainer.LazyResolve<IUserDAL>();
        private IUserDAL _UserDAL => _lazyUserDAL.Value;

        private static readonly Lazy<IAccountDAL> _lazyAccountDAL = ObjectContainer.LazyResolve<IAccountDAL>();
        private IAccountDAL _AccountDAL => _lazyAccountDAL.Value;



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
    }
}
