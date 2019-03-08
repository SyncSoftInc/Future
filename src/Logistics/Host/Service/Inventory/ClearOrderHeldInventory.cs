using Quartz;
using SyncSoft.App.Components;
using SyncSoft.ECP.Quartz;
using SyncSoft.Future.Logistics.Domain.Inventory;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Service.Inventory
{
    public class ClearOrderHeldInventory : JobBase
    {
        private static readonly Lazy<IInventoryService> _lazyInventoryService = ObjectContainer.LazyResolve<IInventoryService>();
        private IInventoryService _InventoryService => _lazyInventoryService.Value;

        protected override Task<string> InnerExecuteAsync(IJobExecutionContext context)
            => _InventoryService.ClearOrderHeldInventoriesAsync();
    }
}
