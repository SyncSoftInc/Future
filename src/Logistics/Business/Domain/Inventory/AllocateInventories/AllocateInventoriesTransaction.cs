using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.Domain.Inventory.TranShared;
using System;
using System.Collections.Generic;

namespace SyncSoft.Future.Logistics.Domain.Inventory.AllocateInventories
{
    public class AllocateInventoriesTransaction : RrTransaction
    {
        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<AllocateInventoriesTransaction>();
        protected override ILogger Logger => _lazyLogger.Value;

        public AllocateInventoriesTransaction(AllocateInventoriesCommand cmd) : base(cmd.CorrelationId)
        {
            base.Context.Items.Add(TranConstants.Context_Items_Command, cmd);
        }

        protected override IEnumerable<RrTransactionActivity> BuildActivities()
        {
            yield return new MasterDBActivity(Context);
            yield return new SyncQueryDBInventoriesActivity(Context);
        }
    }
}