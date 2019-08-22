using SyncSoft.App.Components;
using SyncSoft.Future.Logistics.DataAccess;
using SyncSoft.Future.Logistics.DataAccess.Warehouse;
using SyncSoft.Future.Logistics.DTO.Warehouse;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataFacade.Warehouse
{
    public class WarehouseDF : IWarehouseDF
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<ILogisticsMasterDALFactory> _lazyLogisticsMasterDALFactory = ObjectContainer.LazyResolve<ILogisticsMasterDALFactory>();
        private ILogisticsMasterDALFactory LogisticsMasterDALFactory => _lazyLogisticsMasterDALFactory.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Get  -

        public async Task<MerchantWarehouseDTO> GetSingleAsync(string merchantId, string warehouseId)
        {
            var warehouseDAL = await LogisticsMasterDALFactory.CreateWarehouseDALAsync(merchantId).ConfigureAwait(false);
            return await warehouseDAL.GetAsync(merchantId, warehouseId).ConfigureAwait(false);
        }

        #endregion
    }
}
