namespace SyncSoft.Future.Logistics.Query.Warehouse
{
    public class CountWarehouseQuery : SyncSoft.App.Messaging.Query
    {
        public string Merchant_ID { get; set; }
        public string Warehouse_ID { get; set; }
        public string Name { get; set; }
    }
}
