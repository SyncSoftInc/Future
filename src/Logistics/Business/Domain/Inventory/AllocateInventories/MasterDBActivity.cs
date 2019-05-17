using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.Domain.Inventory.TranShared;
using SyncSoft.Future.Logistics.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Inventory.AllocateInventories
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
            var cmd = Context.Get<AllocateInventoriesCommand>(TranConstants.Context_Items_Command);
            if (cmd.Inventories.IsMissing()) return;
            // ^^^^^^^^^^

            // 备份当前库存
            var backup = await _InventoryMasterDAL.GetInventoriesAsync(cmd.Merchant_ID, cmd.Inventories.Select(x => x.ItemNo)).ConfigureAwait(false);
            Context.Set(TranConstants.Context_Items_BackupInventories, backup);

            // 分配库存，得到分配后计算得到的可用库存
            var availableInventories = await _InventoryMasterDAL.AllocateInventoriesAsync(cmd).ConfigureAwait(false);

            // 将可用库存放入上下文给下一步使用
            Context.Set(TranConstants.Context_Items_AvailableInventories, availableInventories);
        }

        protected override async Task RollbackAsync()
        {
            var cmd = Context.Get<AllocateInventoriesCommand>(TranConstants.Context_Items_Command);
            var backup = Context.Get<IList<InventoryDTO>>(TranConstants.Context_Items_BackupInventories);
            var rollabckCmd = new AllocateInventoriesCommand
            {
                Merchant_ID = cmd.Merchant_ID,
                Warehouse_ID = cmd.Warehouse_ID,
                Inventories = backup
            };

            // 回滚，使用备份库存
            await _InventoryMasterDAL.AllocateInventoriesAsync(cmd).ConfigureAwait(false);
        }
    }
}
