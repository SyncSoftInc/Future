using SyncSoft.App.Messaging;
using System.ComponentModel.DataAnnotations;

namespace SyncSoft.Future.Logistics.Command.Warehouse
{
    public class DeleteWarehouseCommand : AsyncRequestCommand
    {
        [Required]
        public string Merchant_ID { get; set; }
        [Required]
        public string Warehouse_ID { get; set; }
    }
}
