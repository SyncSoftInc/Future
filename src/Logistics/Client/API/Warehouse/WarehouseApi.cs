using SyncSoft.App.Http;
using SyncSoft.Future.Logistics.API.Warehouse;
using SyncSoft.Future.Logistics.DTO.Warehouse;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.API.Warehouse
{
    public class WarehouseApi : LogisticsApi, IWarehouseApi
    {
        public Task<HttpResult<string>> CreateAsync(object cmd, Guid? correlationId, CancellationToken? cancellationToken)
           => base.PostAsync<string>(App.WebApi.Auth.BearerAuthModeEnum.User, "warehouse", cmd, correlationId, cancellationToken);

        public Task<HttpResult<MerchantWarehouseDTO>> GetSingleAsync(string merchatId, string warehouseId, Guid? correlationId, CancellationToken? cancellationToken)
            => base.GetAsync<MerchantWarehouseDTO>(App.WebApi.Auth.BearerAuthModeEnum.User, $"warehouse/{merchatId}/{warehouseId}", correlationId: correlationId, cancellationToken: cancellationToken);

        public Task<HttpResult<string>> UpdateAsync(object cmd, Guid? correlationId, CancellationToken? cancellationToken)
           => base.PutAsync<string>(App.WebApi.Auth.BearerAuthModeEnum.User, "warehouse", cmd, correlationId, cancellationToken);

        public Task<HttpResult<string>> DeleteAsync(object cmd, Guid? correlationId, CancellationToken? cancellationToken)
           => base.DeleteAsync<string>(App.WebApi.Auth.BearerAuthModeEnum.User, "warehouse", cmd, correlationId, cancellationToken);

    }
}
