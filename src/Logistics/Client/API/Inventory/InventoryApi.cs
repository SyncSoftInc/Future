using SyncSoft.App.Http;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.API.Inventory
{
    public class InventoryApi : LogisticsApi, IInventoryApi
    {
        public Task<HttpResult<int>> GetAvailableInventoryAsync(string merchantId, string upc)
           => base.GetAsync<int>(App.WebApi.Auth.BearerAuthModeEnum.User, $"inventory?merchantId={merchantId}&upc={upc}");
    }
}
