using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Warehouse;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.DataAccess
{
    public interface ILogisticsMasterDALFactory
    {
        Task<IInventoryMasterDAL> CreateInventoryDALAsync(string merchantId);
        Task<IWarehouseDAL> CreateWarehouseDALAsync(string merchantId);
    }
}
