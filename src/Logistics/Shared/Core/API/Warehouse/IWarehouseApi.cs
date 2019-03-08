using SyncSoft.App.Http;
using SyncSoft.Future.Logistics.DTO.Warehouse;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.API.Warehouse
{
    public interface IWarehouseApi
    {
        Task<HttpResult<string>> CreateAsync(object cmd, Guid? correlationid = null, CancellationToken? cancellationToken = null);
        Task<HttpResult<MerchantWarehouseDTO>> GetSingleAsync(string merchatId, string warehouseId, Guid? correlationid = null, CancellationToken? cancellationToken = null);
        Task<HttpResult<string>> UpdateAsync(object cmd, Guid? correlationid = null, CancellationToken? cancellationToken = null);
        Task<HttpResult<string>> DeleteAsync(object cmd, Guid? correlationid = null, CancellationToken? cancellationToken = null);
    }
}
