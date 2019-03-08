using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Warehouse
{
    public interface IWarehouseIdFactory
    {
        Task<string> CreateNewAsync();
    }
}
