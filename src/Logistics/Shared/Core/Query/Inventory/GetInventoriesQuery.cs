namespace SyncSoft.Future.Logistics.Query.Inventory
{
    public class GetInventoriesQuery : SyncSoft.App.Messaging.Query
    {
        public string Merchant_ID { get; set; }
        public string Warehouse_ID { get; set; }
        public string ItemNo { get; set; }
    }
}
