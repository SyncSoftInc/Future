using SyncSoft.App.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.API.Inventory
{
    public interface IInventoryApi
    {
        Task<HttpResult<int>> GetAvailableInventoryAsync(string merchantId, string itemNo);
        Task<HttpResult<string>> AllocateInventoriesAsync(object cmd, Guid? correlationId = null, CancellationToken? cancellationToken = null);
        Task<HttpResult<string>> HoldOrderInventoriesAsync(object cmd, Guid? correlationId = null, CancellationToken? cancellationToken = null);
        Task<HttpResult<string>> InventoryShipConfirmAsync(object cmd, Guid? correlationId = null, CancellationToken? cancellationToken = null);
        Task<HttpResult<string>> UnholdOrderInventoriesAsync(object cmd, Guid? correlationId = null, CancellationToken? cancellationToken = null);
    }
}
