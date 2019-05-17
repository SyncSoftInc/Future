using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Collections.Generic;

namespace SyncSoft.Future.Passport.Domain.User.DeleteUser
{
    public class DeleteUserTransaction : TccTransaction
    {
        public const string Parameters_Command = "Command";
        public const string Parameters_User_BackUp = "UserBackup";
        public const string Parameters_Account_BackUp = "AccountBackup";

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<DeleteUserTransaction>();
        public override ILogger Logger => _lazyLogger.Value;

        public DeleteUserTransaction(DeleteUserCommand cmd) : base(cmd.CorrelationId)
        {
            Context.Set(Parameters_Command, cmd);
        }

        protected override IEnumerable<TransactionActivity> BuildActivities()
        {
            yield return new DeleteUserActivity();
            yield return new DeleteAccountActivity();
        }
    }
}
