using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncSoft.Future.Passport.Domain.User.UpdateUser
{
    public class UpdateUserTransaction : RrTransaction
    {
        public const string Parameters_Command = "Command";
        public const string Parameters_User_BackUp = "UserBackup";
        public const string Parameters_Account_BackUp = "AccountBackup";

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<UpdateUserTransaction>();
        protected override ILogger Logger => _lazyLogger.Value;

        public UpdateUserTransaction(UpdateUserCommand cmd) : base(cmd.CorrelationId)
        {
            Context.Items.Add(Parameters_Command, cmd);
        }

        protected override IEnumerable<RrTransactionActivity> BuildActivities()
        {
            yield return new UpdateUserActivity(Context);
            yield return new ChangeUsernameActivity(Context);
            yield return new ChangePasswordActivity(Context);
        }
    }
}
