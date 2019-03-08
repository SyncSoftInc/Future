using SyncSoft.Future.Logistics.DTO.Inventory;
using System.Collections.Generic;

namespace SyncSoft.Future.Logistics.Command.Inventory
{
    public abstract class InventoriesOperationCommand : SyncSoft.App.Messaging.Command
    {
        public string Merchant_ID { get; set; }
        public string Warehouse_ID { get; set; }
        public IList<InventoryDTO> Inventories { get; set; }
    }
}
