using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User.DeleteUser
{
    public class DeleteAccountActivity : TccActivity
    {
        private static readonly Lazy<IAccountDAL> _lazyAccountDAL = ObjectContainer.LazyResolve<IAccountDAL>();
        private IAccountDAL _AccountDAL => _lazyAccountDAL.Value;

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = Context.Get<DeleteUserCommand>(DeleteUserTransaction.Parameters_Command);

            var oldDto = await _AccountDAL.GetAccountAsync(cmd.ID).ConfigureAwait(false);
            if (oldDto.IsNotNull())
            {// 有数据才删除
                Context.Set(DeleteUserTransaction.Parameters_Account_BackUp, oldDto);
                var msgCode = await _AccountDAL.DeleteAccountAsync(cmd.ID).ConfigureAwait(false);
                if (!msgCode.IsSuccess()) throw new Exception(msgCode);
            }
        }

        protected override async Task RollbackAsync()
        {
            var dto = Context.Get<AccountDTO>(DeleteUserTransaction.Parameters_Account_BackUp);
            if (dto.IsNotNull())
            {// 有备份才恢复
                var msgCode = await _AccountDAL.InsertAccountAsync(dto).ConfigureAwait(false);
                if (!msgCode.IsSuccess()) throw new Exception(msgCode);
            }
        }
    }
}
