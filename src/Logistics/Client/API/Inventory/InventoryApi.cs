using SyncSoft.App.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.API.Inventory
{
    public class InventoryApi : LogisticsApi, IInventoryApi
    {
        public Task<HttpResult<int>> GetAvailableInventoryAsync(string merchantId, string upc)
           => base.GetAsync<int>(App.WebApi.Auth.BearerAuthModeEnum.User, $"inventory?merchantId={merchantId}&upc={upc}");

        public Task<HttpResult<string>> AllocateInventoriesAsync(object cmd, Guid? correlationId, CancellationToken? cancellationToken)
           => base.PostAsync<string>(App.WebApi.Auth.BearerAuthModeEnum.User, "inventories", cmd, correlationId, cancellationToken);
    }
}
