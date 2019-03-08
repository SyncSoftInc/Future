using SyncSoft.Future.Logistics.DTO.Warehouse;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataFacade.Warehouse
{
    public interface IWarehouseDF
    {
        Task<MerchantWarehouseDTO> GetSingleAsync(string merchantId, string warehouseId);
    }
}
