using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User.UpdateUser
{
    public class ChangeUsernameActivity : TccActivity
    {
        private static readonly Lazy<IAccountDAL> _lazyAccountDAL = ObjectContainer.LazyResolve<IAccountDAL>();
        private IAccountDAL _AccountDAL => _lazyAccountDAL.Value;

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = Context.Get<UpdateUserCommand>(UpdateUserTransaction.Parameters_Command);
            if (cmd.Username.IsNull()) return;
            // ^^^^^^^^^^

            var oldDto = await _AccountDAL.GetAccountAsync(cmd.ID).ConfigureAwait(false);
            if (oldDto.IsNull()) throw new Exception(MsgCODES.FUT_0000000002);
            //if (!Context.Items.ContainsKey(UpdateUserTransaction.Parameters_Account_Backup))
            //{
            //    Context.Set(UpdateUserTransaction.Parameters_Account_Backup, oldDto);
            //}

            var dto = new AccountDTO
            {
                ID = cmd.ID,
                Username = cmd.Username,
                UpdatedOnUtc = DateTime.UtcNow
            };
            var msgCode = await _AccountDAL.UpdateUsernameAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }

        protected override async Task RollbackAsync()
        {
            var dto = Context.Get<AccountDTO>(UpdateUserTransaction.Parameters_Account_Backup);
            var msgCode = await _AccountDAL.UpdateUsernameAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }
    }
}
