using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.Securities;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User.CreateUser
{
    public class CreateAccountActivity : RrTransactionActivity
    {
        private static readonly Lazy<IAccountDAL> _lazyAccountDAL = ObjectContainer.LazyResolve<IAccountDAL>();
        private IAccountDAL _AccountDAL => _lazyAccountDAL.Value;

        private static readonly Lazy<IPasswordEncryptor> _lazyPasswordEncryptor = ObjectContainer.LazyResolve<IPasswordEncryptor>();
        private IPasswordEncryptor _PasswordEncryptor => _lazyPasswordEncryptor.Value;

        public CreateAccountActivity(RrTransactionContext context) : base(context)
        {
        }

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = (CreateUserCommand)Context.Items[CreateUserTransaction.Parameters_Command];
            var dto = new AccountDTO
            {
                ID = cmd.ID,
                Username = cmd.Username,
                Status = (ECP.Enums.Account.AccountStatusEnum)cmd.Status,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow
            };
            dto.PasswordSalt = _PasswordEncryptor.GeneratePasswordSalt();
            dto.Password = _PasswordEncryptor.HashEncodePassword(cmd.Password, dto.PasswordSalt);

            var msgCode = await _AccountDAL.InsertAccountAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }

        protected override async Task RollbackAsync()
        {
            var cmd = (CreateUserCommand)Context.Items[CreateUserTransaction.Parameters_Command];
            var msgCode = await _AccountDAL.DeleteAccountAsync(cmd.ID).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }
    }
}
