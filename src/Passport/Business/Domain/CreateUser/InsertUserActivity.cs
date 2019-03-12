using System.Threading;
using System.Threading.Tasks;
using SyncSoft.App.Transactions;

namespace SyncSoft.Future.Passport.Domain.CreateUser
{
    public class InsertUserActivity : RrTransactionActivity
    {
        public InsertUserActivity(RrTransactionContext context) : base(context)
        {
        }

        protected override Task RunAsync(CancellationToken? cancellationToken)
        {
            throw new System.NotImplementedException();
        }


        protected override Task RollbackAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
