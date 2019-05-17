using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.Domain.Inventory.TranShared;
using System;
using System.Collections.Generic;

namespace SyncSoft.Future.Logistics.Domain.Inventory.InventoryShipConfirm
{
    public class InventoryShipCancelTransaction : TccTransaction
    {
        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<InventoryShipCancelTransaction>();
        public override ILogger Logger => _lazyLogger.Value;

        public InventoryShipCancelTransaction(InventoryShipConfirmCommand cmd) : base(cmd.CorrelationId)
        {
            base.Context.Set(TranConstants.Context_Items_Command, cmd);
        }

        protected override IEnumerable<TransactionActivity> BuildActivities()
        {
            yield return new MasterDBActivity();
            yield return new SyncQueryDBInventoriesActivity();
        }
    }
}
