using Microsoft.AspNetCore.Mvc;
using SyncSoft.App.Components;
using SyncSoft.ECP.AspNetCore.Mvc.Controllers;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.DataFacade.Inventory;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.WebApi.Controllers
{
    [Area("Inventory")]
    [ApiController]
    public class InventoryController : ApiController
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IInventoryDF> _lazyInventoryDF = ObjectContainer.LazyResolve<IInventoryDF>();
        private IInventoryDF _InventoryDF => _lazyInventoryDF.Value;

        #endregion

        /// <summary>
        /// Allocate inventories.
        /// </summary>
        [HttpPost("inventories")]
        public Task<string> AllocateInventoriesAsync(AllocateInventoriesCommand cmd)
            => base.SendAsync(cmd);

        /// <summary>
        /// Hold order inventory.
        /// </summary>
        [HttpPost("inventories/orderhold")]
        public Task<string> HoldOrderInventoriesAsync(HoldOrderInventoriesCommand cmd)
            => base.SendAsync(cmd);

        /// <summary>
        /// Unhold order inventory.
        /// </summary>
        [HttpDelete("inventories/orderhold")]
        public Task<string> UnholdOrderInventoriesAsync(UnholdOrderInventoriesCommand cmd)
            => base.SendAsync(cmd);

        /// <summary>
        /// Ship confirm, deduct onhand and safe inventory.
        /// </summary>
        [HttpPost("inventories/shipment")]
        public Task<string> UnholdOrderInventoriesAsync(InventoryShipConfirmCommand cmd)
            => base.SendAsync(cmd);

        /// <summary>
        /// Get available inventory
        /// </summary>
        /// <param name="merchantId">Merchant ID</param>
        /// <param name="itemNo">ItemNo</param>
        [HttpGet("inventory")]
        public Task<int> GetAvailableInventoryAsync(string merchantId, string itemNo)
            => _InventoryDF.GetAvailableInventoryAsync(merchantId, itemNo);
    }
}
