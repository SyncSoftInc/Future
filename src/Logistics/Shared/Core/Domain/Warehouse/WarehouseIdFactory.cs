using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Warehouse
{
    public class WarehouseIdFactory : IWarehouseIdFactory
    {
        public Task<string> CreateNewAsync()
        {
            return Task.FromResult(Guid.NewGuid().ToLowerNString());
        }
    }
}
