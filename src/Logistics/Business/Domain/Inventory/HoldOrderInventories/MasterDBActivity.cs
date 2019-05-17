using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.Domain.Inventory.TranShared;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Inventory.HoldOrderInventories
{
    /// <summary>
    /// 主数据库操作
    /// </summary>
    public class MasterDBActivity : TccActivity
    {
        private static readonly Lazy<IInventoryMasterDAL> _lazyInventoryMasterDAL = ObjectContainer.LazyResolve<IInventoryMasterDAL>();
        private IInventoryMasterDAL _InventoryMasterDAL => _lazyInventoryMasterDAL.Value;

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = Context.Get<HoldOrderInventoriesCommand>(TranConstants.Context_Items_Command);
            if (cmd.Inventories.IsMissing()) return;
            // ^^^^^^^^^^

            // 锁库存，得到分配后计算得到的可用库存
            var availableInventories = await _InventoryMasterDAL.HoldOrderInventoriesAsync(cmd).ConfigureAwait(false);

            // 将可用库存放入上下文给下一步使用
            Context.Set(TranConstants.Context_Items_AvailableInventories, availableInventories);
        }

        protected override async Task RollbackAsync()
        {
            var cmd = Context.Get<HoldOrderInventoriesCommand>(TranConstants.Context_Items_Command);
            var rollabckCmd = new UnholdOrderInventoriesCommand
            {
                Merchant_ID = cmd.Merchant_ID,
                Warehouse_ID = cmd.Warehouse_ID,
                Inventories = cmd.Inventories
            };

            // 回滚，解锁库存
            await _InventoryMasterDAL.UnholdOrderInventoriesAsync(rollabckCmd).ConfigureAwait(false);
        }
    }
}
