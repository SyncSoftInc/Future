using SyncSoft.Future.Logistics.Command.Inventory;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Inventory
{
    public interface IInventoryService
    {
        /// <summary>
        /// 分配库存
        /// </summary>
        Task<string> AllocateInventoriesAsync(AllocateInventoriesCommand cmd);
        /// <summary>
        /// 锁定订单库存
        /// </summary>
        Task<string> HoldOrderInventoriesAsync(HoldOrderInventoriesCommand cmd);
        /// <summary>
        /// 解锁订单库存
        /// </summary>
        Task<string> UnholdOrderInventoriesAsync(UnholdOrderInventoriesCommand cmd);
        /// <summary>
        /// 库存出运确认
        /// </summary>
        Task<string> InventoryShipConfirmAsync(InventoryShipConfirmCommand cmd);

        /// <summary>
        /// 清理数量为0的订单锁定库存数据
        /// </summary>
        Task<string> ClearOrderHeldInventoriesAsync();
    }
}
