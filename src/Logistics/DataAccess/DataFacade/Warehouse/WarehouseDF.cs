using SyncSoft.App.Components;
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

        private static readonly Lazy<IWarehouseDAL> _lazyWarehouseDAL = ObjectContainer.LazyResolve<IWarehouseDAL>();
        private IWarehouseDAL _WarehouseDAL => _lazyWarehouseDAL.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Get  -

        public Task<MerchantWarehouseDTO> GetSingleAsync(string merchantId, string warehouseId)
        {
            return _WarehouseDAL.GetAsync(merchantId, warehouseId);
        }

        #endregion
    }
}
