using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Collections.Generic;

namespace SyncSoft.Future.Passport.Domain.User.CreateUser
{
    public class CreateUserTransaction : TccTransaction
    {
        public const string Parameters_Command = "Command";

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<CreateUserTransaction>();
        public override ILogger Logger => _lazyLogger.Value;

        public CreateUserTransaction(CreateUserCommand cmd) : base(cmd.CorrelationId)
        {
            Context.Set(Parameters_Command, cmd);
        }

        protected override IEnumerable<TransactionActivity> BuildActivities()
        {
            yield return new CreateAccountActivity();
            yield return new CreateUserActivity();
        }
    }
}
