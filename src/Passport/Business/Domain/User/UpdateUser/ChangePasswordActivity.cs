using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.Securities;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User.UpdateUser
{
    public class ChangePasswordActivity : RrTransactionActivity
    {
        private static readonly Lazy<IAccountDAL> _lazyAccountDAL = ObjectContainer.LazyResolve<IAccountDAL>();
        private IAccountDAL _AccountDAL => _lazyAccountDAL.Value;

        private static readonly Lazy<IPasswordEncryptor> _lazyPasswordEncryptor = ObjectContainer.LazyResolve<IPasswordEncryptor>();
        private IPasswordEncryptor _PasswordEncryptor => _lazyPasswordEncryptor.Value;

        public ChangePasswordActivity(RrTransactionContext context) : base(context)
        {
        }

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = (UpdateUserCommand)Context.Items[UpdateUserTransaction.Parameters_Command];
            if (cmd.Password.IsMissing()) return;
            // ^^^^^^^^^^

            var oldDto = await _AccountDAL.GetAccountAsync(cmd.ID).ConfigureAwait(false);
            if (oldDto.IsNull()) throw new Exception(MsgCODES.FUT_0000000002);
            if (!Context.Items.ContainsKey(UpdateUserTransaction.Parameters_Account_Backup))
            {
                Context.Items.Add(UpdateUserTransaction.Parameters_Account_Backup, oldDto);
            }

            var dto = new AccountDTO
            {
                ID = cmd.ID,
                LoginFailedCount = 0,
                UpdatedOnUtc = DateTime.UtcNow
            };
            dto.PasswordSalt = _PasswordEncryptor.GeneratePasswordSalt();
            dto.Password = _PasswordEncryptor.HashEncodePassword(cmd.Password, dto.PasswordSalt);
            var msgCode = await _AccountDAL.UpdateLoginInfoAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }

        protected override async Task RollbackAsync()
        {
            var dto = (AccountDTO)Context.Items[UpdateUserTransaction.Parameters_Account_Backup];
            var msgCode = await _AccountDAL.UpdatePasswordAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }
    }
}
