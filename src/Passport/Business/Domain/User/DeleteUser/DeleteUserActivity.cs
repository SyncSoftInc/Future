using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.Command.User;
using SyncSoft.Future.Passport.DataAccess.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User.DeleteUser
{
    public class DeleteUserActivity : TccActivity
    {
        private static readonly Lazy<IUserDAL> _lazyUserDAL = ObjectContainer.LazyResolve<IUserDAL>();
        private IUserDAL _UserDAL => _lazyUserDAL.Value;

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = Context.Get<DeleteUserCommand>(DeleteUserTransaction.Parameters_Command);

            var oldDto = await _UserDAL.GetUserAsync(cmd.ID).ConfigureAwait(false);
            if (oldDto.IsNull()) throw new Exception(MsgCODES.FUT_0000000002);
            Context.Set(DeleteUserTransaction.Parameters_User_BackUp, oldDto);

            var msgCode = await _UserDAL.DeleteUserAsync(cmd.ID).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }

        protected override async Task RollbackAsync()
        {
            var dto = Context.Get<UserDTO>(DeleteUserTransaction.Parameters_User_BackUp);
            var msgCode = await _UserDAL.InsertUserAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }
    }
}
