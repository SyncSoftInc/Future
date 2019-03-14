using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataAccess.Inventory
{
    public interface IInventoryQueryDAL
    {
        /// <summary>
        /// 获取可用库存
        /// </summary>
        Task<int> GetAvailableInventoryAsync(string merchantId, string itemNo);
        /// <summary>
        /// 批量获取可用库存
        /// </summary>
        Task<IDictionary<string, int>> GetAvailableInventoriesAsync(string merchantId, params string[] itemNos);
        /// <summary>
        /// 同步库存
        /// </summary>
        Task SyncInventoriesAsync(string merchantId, KeyValuePair<string, int>[] inventories);
    }
}
