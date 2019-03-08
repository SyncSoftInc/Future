using SyncSoft.App.Components;
using SyncSoft.App.Messaging;
using SyncSoft.Future.Logistics.Command.Warehouse;
using SyncSoft.Future.Logistics.Domain.Warehouse;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.CommandHandler.Warehouse
{
    public class WarehouseCommandHandler :
        IConsumer<CreateWarehouseCommand>
        , IConsumer<UpdateWarehouseCommand>
        , IConsumer<DeleteWarehouseCommand>
    {
        private static readonly Lazy<IWarehouseService> _lazyWarehouseService = ObjectContainer.LazyResolve<IWarehouseService>();
        private IWarehouseService _WarehouseService => _lazyWarehouseService.Value;

        public async Task<object> HandleAsync(IContext<CreateWarehouseCommand> context)
            => await _WarehouseService.CreateAsync(context.Message).ConfigureAwait(false);

        public async Task<object> HandleAsync(IContext<UpdateWarehouseCommand> context)
            => await _WarehouseService.UpdateAsync(context.Message).ConfigureAwait(false);

        public async Task<object> HandleAsync(IContext<DeleteWarehouseCommand> context)
            => await _WarehouseService.DeleteAsync(context.Message).ConfigureAwait(false);
    }
}
