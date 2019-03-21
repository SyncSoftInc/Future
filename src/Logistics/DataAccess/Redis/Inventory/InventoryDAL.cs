using SyncSoft.Future.Logistics.DataAccess.Inventory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Redis.Inventory
{
    public class InventoryDAL : IInventoryQueryDAL
    {
        private const string _Prefix = "INVENTORY:";
        private readonly IInventoryDB _db;

        public InventoryDAL(IInventoryDB db)
        {
            _db = db;
        }

        public async Task<int> GetAvailableInventoryAsync(string merchantId, string itemNo)
        {
            return await _db.HGetAsync<int>(_Prefix + merchantId, itemNo).ConfigureAwait(false);
        }

        public async Task<IDictionary<string, int>> GetAvailableInventoriesAsync(string merchantId, params string[] itemNos)
        {
            var results = await _db.HMGetAsync<int>(_Prefix + merchantId, itemNos).ConfigureAwait(false);

            return Enumerable.Range(0, itemNos.Length).ToDictionary(i => itemNos[i], i => results[i]);
        }

        public async Task SyncInventoriesAsync(string merchantId, KeyValuePair<string, int>[] inventories)
        {
            var data = inventories.Select(x => new KeyValuePair<string, object>(x.Key, x.Value)).ToArray();
            await _db.HMSetAsync(_Prefix + merchantId, data).ConfigureAwait(false);
        }
    }
}
