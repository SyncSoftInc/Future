using SyncSoft.App.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.API.Inventory
{
    public class InventoryApi : LogisticsApi, IInventoryApi
    {
        public Task<HttpResult<int>> GetAvailableInventoryAsync(string merchantId, string itemNo)
           => base.GetAsync<int>(App.WebApi.Auth.BearerAuthModeEnum.User, $"inventory?merchantId={merchantId}&itemNo={itemNo}");

        public Task<HttpResult<string>> AllocateInventoriesAsync(object cmd, Guid? correlationId, CancellationToken? cancellationToken)
           => base.PostAsync<string>(App.WebApi.Auth.BearerAuthModeEnum.User, "inventories", cmd, correlationId, cancellationToken);

        public Task<HttpResult<string>> HoldOrderInventoriesAsync(object cmd, Guid? correlationId = null, CancellationToken? cancellationToken = null)
           => base.PostAsync<string>(App.WebApi.Auth.BearerAuthModeEnum.User, "inventories/orderhold", cmd, correlationId, cancellationToken);

        public Task<HttpResult<string>> InventoryShipConfirmAsync(object cmd, Guid? correlationId = null, CancellationToken? cancellationToken = null)
           => base.PostAsync<string>(App.WebApi.Auth.BearerAuthModeEnum.User, "inventories/shipment", cmd, correlationId, cancellationToken);

        public Task<HttpResult<string>> UnholdOrderInventoriesAsync(object cmd, Guid? correlationId = null, CancellationToken? cancellationToken = null)
           => base.PostAsync<string>(App.WebApi.Auth.BearerAuthModeEnum.User, "inventories/orderhold", cmd, correlationId, cancellationToken);
    }
}
