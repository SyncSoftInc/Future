using SyncSoft.Future.Logistics.Command.Warehouse;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Warehouse
{
    public interface IWarehouseService
    {
        Task<string> CreateAsync(CreateWarehouseCommand cmd);
        Task<string> UpdateAsync(UpdateWarehouseCommand cmd);
        Task<string> DeleteAsync(DeleteWarehouseCommand cmd);
    }
}
