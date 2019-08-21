using SyncSoft.App.Components;
using SyncSoft.App.Configurations;
using SyncSoft.Future.Logistics.DataAccess;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Warehouse;
using SyncSoft.Future.Logistics.MySql.Inventory;
using SyncSoft.Future.Logistics.MySql.Warehouse;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.MySql
{
    public class LogisticsMasterDALFactory : ILogisticsMasterDALFactory
    {
        private static readonly Lazy<IConfigProvider> _lazyConfigProvider = ObjectContainer.LazyResolve<IConfigProvider>();
        private IConfigProvider ConfigProvider => _lazyConfigProvider.Value;

        private readonly string _defaultConnStrName;

        public LogisticsMasterDALFactory(string defaultConnStrName)
        {
            _defaultConnStrName = defaultConnStrName;
        }

        private async Task<ILogisticsDB> CreateDBAsync(string merchantId)
        {
            var connStr = await GetConnectStringAsync(merchantId);

            return new LogisticsDB(connStr);
        }

        private Task<string> GetConnectStringAsync(string merchantId)
        {
            // Todo: 获取Merchant配置的连接字符串，如果没有配置，则使用默认
            var connStr = ConfigProvider.GetConnectionString(_defaultConnStrName);
            return Task.FromResult(connStr);
        }

        public async Task<IInventoryMasterDAL> CreateInventoryDALAsync(string merchantId)
        {
            var db = await CreateDBAsync(merchantId);
            return new InventoryDAL(db);
        }

        public async Task<IWarehouseDAL> CreateWarehouseDALAsync(string merchantId)
        {
            var db = await CreateDBAsync(merchantId);
            return new WarehouseDAL(db);
        }
    }
}
