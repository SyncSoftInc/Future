using SyncSoft.App.Components;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataFacade.Inventory
{
    public class InventoryDF : IInventoryDF
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IInventoryQueryDAL> _lazyInventoryQueryDAL = ObjectContainer.LazyResolve<IInventoryQueryDAL>();
        private IInventoryQueryDAL _InventoryQueryDAL => _lazyInventoryQueryDAL.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  GetAvailableInventory  -

        public Task<int> GetAvailableInventoryAsync(string merchantId, string itemNo)
            => _InventoryQueryDAL.GetAvailableInventoryAsync(merchantId, itemNo);

        #endregion
    }
}
