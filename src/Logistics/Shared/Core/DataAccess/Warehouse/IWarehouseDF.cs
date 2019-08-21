using SyncSoft.Future.Logistics.DTO.Warehouse;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataAccess.Warehouse
{
    public interface IWarehouseDF
    {
        Task<MerchantWarehouseDTO> GetSingleAsync(string merchantId, string warehouseId);
    }
}
