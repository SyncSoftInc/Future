using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Collections.Generic;

namespace SyncSoft.Future.Passport.Domain.User.UserSaveProfile
{
    public class UserSaveProfileTransaction : RrTransaction
    {
        public const string Parameters_Command = "Command";
        public const string Parameters_Account_Backup = "AccountBackup";
        public const string Parameters_User_Backup = "UserBackup";

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<UserSaveProfileTransaction>();
        protected override ILogger Logger => _lazyLogger.Value;

        public UserSaveProfileTransaction(UserSaveProfileCommand cmd) : base(cmd.CorrelationId)
        {
            Context.Items.Add(Parameters_Command, cmd);
        }

        protected override IEnumerable<RrTransactionActivity> BuildActivities()
        {
            yield return new UpdateUserActivity(Context);
            yield return new ChangePasswordActivity(Context);
        }
    }
}
