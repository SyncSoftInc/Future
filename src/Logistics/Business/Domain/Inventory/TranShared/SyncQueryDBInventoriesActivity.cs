using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Inventory.TranShared
{
    /// <summary>
    /// 读取数据库操作
    /// </summary>
    public class SyncQueryDBInventoriesActivity : TccActivity
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IInventoryQueryDAL> _lazyInventoryQueryDAL = ObjectContainer.LazyResolve<IInventoryQueryDAL>();
        private IInventoryQueryDAL _InventoryQueryDAL => _lazyInventoryQueryDAL.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Property(ies)  -

        public override int RunOrdinal => 1;

        #endregion
        // *******************************************************************************************************************************
        #region -  Run  -

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = Context.Get<InventoriesOperationCommand>(TranConstants.Context_Items_Command);
            var availableInventories = Context.Get<IList<InventoryDTO>>(TranConstants.Context_Items_AvailableInventories);
            if (availableInventories.IsMissing()) return;
            // ^^^^^^^^^^

            // 更新库存
            var kvps = availableInventories.ToKeyValuePairs(x => x.ItemNo, x => x.Qty);
            await _InventoryQueryDAL.SyncInventoriesAsync(cmd.Merchant_ID, kvps).ConfigureAwait(false);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  Rollback  -

        protected override Task RollbackAsync()
        {// 此步骤为最后一步，如果执行失败，不需要回滚
            throw new NotImplementedException();
        }

        #endregion
    }
}
