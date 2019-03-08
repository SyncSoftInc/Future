using SyncSoft.App.Http;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.API.Inventory
{
    public interface IInventoryApi
    {
        Task<HttpResult<int>> GetAvailableInventoryAsync(string merchantId, string upc);
    }
}
