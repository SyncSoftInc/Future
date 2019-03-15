using Microsoft.AspNetCore.Mvc;
using SyncSoft.App.Components;
using SyncSoft.ECP.AspNetCore.Mvc.Controllers;
using SyncSoft.Future.Logistics.Command.Warehouse;
using SyncSoft.Future.Logistics.DataFacade.Warehouse;
using SyncSoft.Future.Logistics.DTO.Warehouse;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.WebApi.Controllers
{
    [Area("Warehouse")]
    [ApiController]
    public class WarehouseController : ApiController
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IWarehouseDF> _lazyWarehouseDF = ObjectContainer.LazyResolve<IWarehouseDF>();
        private IWarehouseDF _WarehouseDF => _lazyWarehouseDF.Value;

        #endregion

        [HttpGet("warehouse/{merchatId}/{warehouseId}")]
        public Task<MerchantWarehouseDTO> GetSingleAsync(string merchatId, string warehouseId)
        {
            return _WarehouseDF.GetSingleAsync(merchatId, warehouseId);
        }

        [HttpPost("warehouse")]
        public async Task<string> CreateAsync(CreateWarehouseCommand cmd)
        {
            var mr = await base.PublishAsync(cmd).ConfigureAwait(false);
            return mr.MsgCode;
        }

        [HttpPut("warehouse")]
        public async Task<string> UpdateAsync(UpdateWarehouseCommand cmd)
        {
            var mr = await base.PublishAsync(cmd).ConfigureAwait(false);
            return mr.MsgCode;
        }

        [HttpDelete("warehouse")]
        public async Task<string> DeleteAsync(DeleteWarehouseCommand cmd)
        {
            var mr = await base.PublishAsync(cmd).ConfigureAwait(false);
            return mr.MsgCode;
        }
    }
}
