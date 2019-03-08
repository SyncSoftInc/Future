using SyncSoft.Future.Logistics.Command.Inventory;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Inventory
{
    public interface IInventoryService
    {
        Task<string> AllocateInventoriesAsync(AllocateInventoriesCommand cmd);
        Task<string> HoldOrderInventoriesAsync(HoldOrderInventoriesCommand cmd);
        Task<string> UnholdOrderInventoriesAsync(UnholdOrderInventoriesCommand cmd);
        Task<string> InventoryShipConfirmAsync(InventoryShipConfirmCommand cmd);
    }
}
