using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User.UserSaveProfile
{
    public class ChangePasswordActivity : RrTransactionActivity
    {
        private static readonly Lazy<IAccountDAL> _lazyAccountDAL = ObjectContainer.LazyResolve<IAccountDAL>();
        private IAccountDAL _AccountDAL => _lazyAccountDAL.Value;

        public ChangePasswordActivity(RrTransactionContext context) : base(context)
        {
        }

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = (UserSaveProfileCommand)Context.Items[UserSaveProfileTransaction.Parameters_Command];

            var dto = new AccountDTO
            {
                ID = cmd.ID,
            };
        }

        protected override async Task RollbackAsync()
        {
        }
    }
}
