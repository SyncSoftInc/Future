using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.DTO.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataAccess.Inventory
{
    public interface IInventoryMasterDAL
    {
        /// <summary>
        /// 分配库存，没有就新增，有则覆盖数量
        /// </summary>
        Task<IList<InventoryDTO>> AllocateInventoriesAsync(AllocateInventoriesCommand cmd);
        /// <summary>
        /// 锁住订单库存
        /// </summary>
        Task<IList<InventoryDTO>> HoldOrderInventoriesAsync(HoldOrderInventoriesCommand cmd);
        /// <summary>
        /// 解锁订单库存
        /// </summary>
        Task<IList<InventoryDTO>> UnholdOrderInventoriesAsync(UnholdOrderInventoriesCommand cmd);
        /// <summary>
        /// 确认出运，同时减少OnHand和Hold库存
        /// </summary>
        Task<IList<InventoryDTO>> InventoryShipConfirmAsync(InventoryShipConfirmCommand cmd);
        /// <summary>
        /// 撤销出运，同时增加OnHand和Hold库存
        /// </summary>
        Task<IList<InventoryDTO>> InventoryShipCancelAsync(InventoryShipCancelCommand cmd);

        /// <summary>
        /// 清理数量为0的订单锁定库存数据
        /// </summary>
        Task<string> ClearOrderHeldInventoriesAsync();

        /// <summary>
        /// 批量获取库存
        /// </summary>
        Task<IList<InventoryDTO>> GetInventoriesAsync(string merchantId, IEnumerable<string> itemNos);
        /// <summary>
        /// 批量获取可用库存
        /// </summary>
        Task<IDictionary<string, int>> GetAvailableInventoriesAsync(string merchantId, IEnumerable<string> itemNos);
    }
}
