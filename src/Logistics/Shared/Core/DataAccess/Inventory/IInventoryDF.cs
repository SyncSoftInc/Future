using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataAccess.Inventory
{
    public interface IInventoryDF
    {
        Task<int> GetAvailableInventoryAsync(string merchantId, string itemNo);
    }
}
