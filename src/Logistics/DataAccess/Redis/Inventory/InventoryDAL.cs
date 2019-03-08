using SyncSoft.Future.Logistics.DataAccess.Inventory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Redis.Inventory
{
    public class InventoryDAL : IInventoryQueryDAL
    {
        private readonly IInventoryDB _db;

        public InventoryDAL(IInventoryDB db)
        {
            _db = db;
        }

        public async Task<int> GetAvailableInventoryAsync(string merchantId, string upc)
        {
            return await _db.HGetAsync<int>(merchantId, upc).ConfigureAwait(false);
        }

        public async Task<IDictionary<string, int>> GetAvailableInventoriesAsync(string merchantId, params string[] upcs)
        {
            var results = await _db.HMGetAsync<int>(merchantId, upcs).ConfigureAwait(false);

            return Enumerable.Range(0, upcs.Length).ToDictionary(i => upcs[i], i => results[i]);
        }

        public async Task SyncInventoriesAsync(string merchantId, KeyValuePair<string, int>[] inventories)
        {
            var data = inventories.Select(x => new KeyValuePair<string, object>(x.Key, x.Value)).ToArray();
            await _db.HMSetAsync(merchantId, data).ConfigureAwait(false);
        }
    }
}
