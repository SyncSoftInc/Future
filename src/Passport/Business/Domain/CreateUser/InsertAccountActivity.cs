using SyncSoft.App.Transactions;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.CreateUser
{
    public class InsertAccountActivity : RrTransactionActivity
    {
        public InsertAccountActivity(RrTransactionContext context) : base(context)
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
