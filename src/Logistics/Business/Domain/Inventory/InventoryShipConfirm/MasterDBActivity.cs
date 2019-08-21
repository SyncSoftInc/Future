using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.DataAccess;
using SyncSoft.Future.Logistics.Domain.Inventory.TranShared;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Inventory.InventoryShipConfirm
{
    /// <summary>
    /// 主数据库操作
    /// </summary>
    public class MasterDBActivity : TccActivity
    {
        private static readonly Lazy<ILogisticsMasterDALFactory> _lazyLogisticsMasterDALFactory = ObjectContainer.LazyResolve<ILogisticsMasterDALFactory>();
        private ILogisticsMasterDALFactory LogisticsMasterDALFactory => _lazyLogisticsMasterDALFactory.Value;

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = Context.Get<InventoryShipConfirmCommand>(TranConstants.Context_Items_Command);
            if (cmd.Inventories.IsMissing()) return;
            // ^^^^^^^^^^

            var inventoryMasterDAL = await LogisticsMasterDALFactory.CreateInventoryDALAsync(cmd.Merchant_ID).ConfigureAwait(false);
            // 库存出运确认，得到分配后计算得到的可用库存
            var availableInventories = await inventoryMasterDAL.InventoryShipConfirmAsync(cmd).ConfigureAwait(false);

            // 将可用库存放入上下文给下一步使用
            Context.Set(TranConstants.Context_Items_AvailableInventories, availableInventories);
        }

        protected override async Task RollbackAsync()
        {
            var cmd = Context.Get<InventoryShipConfirmCommand>(TranConstants.Context_Items_Command);
            var rollabckCmd = new InventoryShipCancelCommand
            {
                Merchant_ID = cmd.Merchant_ID,
                Warehouse_ID = cmd.Warehouse_ID,
                Inventories = cmd.Inventories
            };

            var inventoryMasterDAL = await LogisticsMasterDALFactory.CreateInventoryDALAsync(cmd.Merchant_ID).ConfigureAwait(false);
            // 回滚，撤销库存出运
            await inventoryMasterDAL.InventoryShipCancelAsync(rollabckCmd).ConfigureAwait(false);
        }
    }
}
