using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Transactions;
using System;
using System.Collections.Generic;

namespace SyncSoft.Future.Passport.Domain.CreateUser
{
    public class CreateUserTransaction : RrTransaction
    {
        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<CreateUserTransaction>();
        protected override ILogger Logger => _lazyLogger.Value;

        public CreateUserTransaction(Guid correlationId) : base(correlationId)
        {
        }

        protected override IEnumerable<RrTransactionActivity> BuildActivities()
        {
            yield return new InsertAccountActivity(Context);
            yield return new InsertUserActivity(Context);
        }
    }
}
