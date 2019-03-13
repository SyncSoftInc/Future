using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Collections.Generic;

namespace SyncSoft.Future.Passport.Domain.User.CreateUser
{
    public class CreateUserTransaction : RrTransaction
    {
        public const string Parameters_Command = "Command";

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<CreateUserTransaction>();
        protected override ILogger Logger => _lazyLogger.Value;

        public CreateUserTransaction(CreateUserCommand cmd) : base(cmd.CorrelationId)
        {
            Context.Items.Add(Parameters_Command, cmd);
        }

        protected override IEnumerable<RrTransactionActivity> BuildActivities()
        {
            yield return new CreateAccountActivity(Context);
            yield return new CreateUserActivity(Context);
        }
    }
}
