using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Collections.Generic;

namespace SyncSoft.Future.Passport.Domain.User.UserSaveProfile
{
    public class UserSaveProfileTransaction : TccTransaction
    {
        public const string Parameters_Command = "Command";
        public const string Parameters_Account_Backup = "AccountBackup";
        public const string Parameters_User_Backup = "UserBackup";

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<UserSaveProfileTransaction>();
        public override ILogger Logger => _lazyLogger.Value;

        public UserSaveProfileTransaction(UserSaveProfileCommand cmd) : base(cmd.CorrelationId)
        {
            Context.Set(Parameters_Command, cmd);
        }

        protected override IEnumerable<TransactionActivity> BuildActivities()
        {
            yield return new UpdateUserActivity();
            yield return new ChangePasswordActivity();
        }
    }
}
