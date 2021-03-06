﻿using SyncSoft.App.Components;
using SyncSoft.App.Messaging;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.Domain.Inventory;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.CommandHandler.Inventory
{
    public class InventoryCommandHandler :
          IConsumer<AllocateInventoriesCommand>
        , IConsumer<HoldOrderInventoriesCommand>
        , IConsumer<UnholdOrderInventoriesCommand>
        , IConsumer<InventoryShipConfirmCommand>
    {
        private static readonly Lazy<IInventoryService> _lazyInventoryService = ObjectContainer.LazyResolve<IInventoryService>();
        private IInventoryService _InventoryService => _lazyInventoryService.Value;

        public async Task<object> HandleAsync(IContext<AllocateInventoriesCommand> context)
            => await _InventoryService.AllocateInventoriesAsync(context.Message).ConfigureAwait(false);

        public async Task<object> HandleAsync(IContext<HoldOrderInventoriesCommand> context)
            => await _InventoryService.HoldOrderInventoriesAsync(context.Message).ConfigureAwait(false);

        public async Task<object> HandleAsync(IContext<InventoryShipConfirmCommand> context)
            => await _InventoryService.InventoryShipConfirmAsync(context.Message).ConfigureAwait(false);

        public async Task<object> HandleAsync(IContext<UnholdOrderInventoriesCommand> context)
            => await _InventoryService.UnholdOrderInventoriesAsync(context.Message).ConfigureAwait(false);
    }
}
