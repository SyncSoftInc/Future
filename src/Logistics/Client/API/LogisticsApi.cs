using SyncSoft.ECP.WebApi.ApiProxies;

namespace SyncSoft.Future.Logistics.API
{
    public abstract class LogisticsApi : ECPApiProxyBase
    {
        protected override string UriKey => "warehouseapi";
    }
}
