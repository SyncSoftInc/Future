using SyncSoft.App.Messaging;
using System.Collections.Generic;

namespace SyncSoft.Future.Logistics.Command.Inventory
{
    public class HoldOrderInventoriesCommand : InventoriesOperationCommand
    {
        public string OrderNo { get; set; }
    }
}
