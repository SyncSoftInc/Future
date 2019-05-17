using SyncSoft.App.Components;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.Domain.Inventory.AllocateInventories;
using SyncSoft.Future.Logistics.Domain.Inventory.HoldOrderInventories;
using SyncSoft.Future.Logistics.Domain.Inventory.InventoryShipConfirm;
using SyncSoft.Future.Logistics.Domain.Inventory.UnholdOrderInventories;
using SyncSoft.Future.Logistics.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Inventory
{
    public class InventoryService : IInventoryService
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IInventoryMasterDAL> _lazyInventoryMasterDAL = ObjectContainer.LazyResolve<IInventoryMasterDAL>();
        private IInventoryMasterDAL _InventoryMasterDAL => _lazyInventoryMasterDAL.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  AllocateInventories  -

        /// <summary>
        /// 分配库存
        /// </summary>
        /// <remarks>
        /// 插入库存Qty和SafeQty，如果已经存在则直接更新
        /// </remarks>
        public async Task<string> AllocateInventoriesAsync(AllocateInventoriesCommand cmd)
        {
            var msgCode = ValidateCommand(cmd);
            if (!msgCode.IsSuccess()) return msgCode;
            // ^^^^^^^^^^

            // 检查参数正确性
            foreach (var item in cmd.Inventories)
            {
                if (item.SafeQty > item.Qty) return MsgCodes.WH_0000000010;
                // ^^^^^^^^^^

                if (item.SafeQty == 0) item.SafeQty = item.Qty; // 使用实际库存作为安全库存
            }

            // 开始执行事务
            var tran = new AllocateInventoriesTransaction(cmd);
            await tran.RunAsync().ConfigureAwait(false);


            return MsgCodes.SUCCESS;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  HoldInventories  -

        /// <summary>
        /// 锁定订单库存
        /// </summary>
        public async Task<string> HoldOrderInventoriesAsync(HoldOrderInventoriesCommand cmd)
        {
            var msgCode = ValidateCommand(cmd);
            if (!msgCode.IsSuccess()) return msgCode;
            // ^^^^^^^^^^

            msgCode = await EnsureInventoriesSufficientAsync(cmd.Merchant_ID, cmd.Inventories).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) return msgCode;
            // ^^^^^^^^^^

            // 开始执行事务
            var tran = new HoldOrderInventoriesTransaction(cmd);
            await tran.RunAsync().ConfigureAwait(false);
            return MsgCodes.SUCCESS;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UnholdInventories  -

        /// <summary>
        /// 解锁订单库存
        /// </summary>
        public async Task<string> UnholdOrderInventoriesAsync(UnholdOrderInventoriesCommand cmd)
        {
            var msgCode = ValidateCommand(cmd);
            if (!msgCode.IsSuccess()) return msgCode;
            // ^^^^^^^^^^

            // 开始执行事务
            var tran = new UnholdOrderInventoriesTransaction(cmd);
            await tran.RunAsync().ConfigureAwait(false);
            return MsgCodes.SUCCESS;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  InventoryShipConfirm  -

        /// <summary>
        /// 库存出运确认
        /// </summary>
        public async Task<string> InventoryShipConfirmAsync(InventoryShipConfirmCommand cmd)
        {
            var msgCode = ValidateCommand(cmd);
            if (!msgCode.IsSuccess()) return msgCode;
            // ^^^^^^^^^^

            // 开始执行事务
            var tran = new InventoryShipCancelTransaction(cmd);
            await tran.RunAsync().ConfigureAwait(false);
            return MsgCodes.SUCCESS;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  MyRegion  -

        /// <summary>
        /// 清理数量为0的订单锁定库存数据
        /// </summary>
        public Task<string> ClearOrderHeldInventoriesAsync()
            => _InventoryMasterDAL.ClearOrderHeldInventoriesAsync();

        #endregion
        // *******************************************************************************************************************************
        #region -  Validation  -

        /// <summary>
        /// 检查命令
        /// </summary>
        private string ValidateCommand(InventoriesOperationCommand cmd)
        {
            if (cmd.Inventories.IsMissing()) return MsgCodes.WH_0000000004;
            // ^^^^^^^^^^
            foreach (var item in cmd.Inventories)
            {
                if (item.Qty < 0 || item.SafeQty < 0) return MsgCodes.WH_0000000009;
                // ^^^^^^^^^^
            }

            return MsgCodes.SUCCESS;
        }

        /// <summary>
        /// 确保不超卖
        /// </summary>
        private async Task<string> EnsureInventoriesSufficientAsync(string merchantId, IEnumerable<InventoryDTO> operationInventories)
        {
            var availableInventories = await _InventoryMasterDAL.GetAvailableInventoriesAsync(merchantId, operationInventories.Select(x => x.ItemNo)).ConfigureAwait(false);
            var hasInsufficientInventory = operationInventories.Any(x => x.Qty > availableInventories[x.ItemNo]);
            if (hasInsufficientInventory) return MsgCodes.WH_0000000007;
            // ^^^^^^^^^^ 库存不足

            return MsgCodes.SUCCESS;
        }

        #endregion
    }
}
