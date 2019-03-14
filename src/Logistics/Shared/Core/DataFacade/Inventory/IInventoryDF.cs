using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataFacade.Inventory
{
    public interface IInventoryDF
    {
        Task<int> GetAvailableInventoryAsync(string merchantId, string itemNo);
    }
}
