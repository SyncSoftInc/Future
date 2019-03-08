using SyncSoft.App.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.API.Inventory
{
    public interface IInventoryApi
    {
        Task<HttpResult<int>> GetAvailableInventoryAsync(string merchantId, string upc);
        Task<HttpResult<string>> AllocateInventoriesAsync(object cmd, Guid? correlationId = null, CancellationToken? cancellationToken = null);
    }
}
