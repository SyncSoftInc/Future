using SyncSoft.App.Messaging;
using System.ComponentModel.DataAnnotations;

namespace SyncSoft.Future.Logistics.Command.Warehouse
{
    public class CreateWarehouseCommand : AsyncRequestCommand
    {
        public string ID { get; set; }
        [Required]
        public string Merchant_ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
