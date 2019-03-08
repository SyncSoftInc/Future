using SyncSoft.App.Messaging;
using System.Collections.Generic;

namespace SyncSoft.Future.Logistics.Command.Inventory
{
    public class InventoryShipCancelCommand : InventoriesOperationCommand
    {
        public string OrderNo { get; set; }
    }
}
