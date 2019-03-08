using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataAccess.Inventory
{
    public interface IInventoryQueryDAL
    {
        /// <summary>
        /// 获取可用库存
        /// </summary>
        Task<int> GetAvailableInventoryAsync(string merchantId, string upc);
        /// <summary>
        /// 批量获取可用库存
        /// </summary>
        Task<IDictionary<string, int>> GetAvailableInventoriesAsync(string merchantId, params string[] upcs);
        /// <summary>
        /// 同步库存
        /// </summary>
        Task SyncInventoriesAsync(string merchantId, KeyValuePair<string, int>[] inventories);
    }
}
