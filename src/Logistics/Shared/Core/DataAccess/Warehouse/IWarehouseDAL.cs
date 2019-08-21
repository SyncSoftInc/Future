using SyncSoft.Future.Logistics.DTO.Warehouse;
using SyncSoft.Future.Logistics.Query.Warehouse;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataAccess.Warehouse
{
    public interface IWarehouseDAL
    {
        Task<MerchantWarehouseDTO> GetAsync(string merchantId, string warehouseId);
        Task<string> InsertAsync(MerchantWarehouseDTO dto);
        Task<string> UpdateAsync(MerchantWarehouseDTO dto);
        Task<string> DeleteAsync(MerchantWarehouseDTO dto);
        Task<int> CountWarehouseAsync(CountWarehouseQuery query);
    }
}
