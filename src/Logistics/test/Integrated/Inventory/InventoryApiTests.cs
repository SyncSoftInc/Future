using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Messaging;
using SyncSoft.Future.Logistics.API.Inventory;
using SyncSoft.Future.Logistics.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedTest.Inventory
{
    public class InventoryApiTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IInventoryApi> _lazyInventoryApi = ObjectContainer.LazyResolve<IInventoryApi>();
        private IInventoryApi _InventoryApi => _lazyInventoryApi.Value;

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger(nameof(InventoryApiTests));
        private ILogger _Logger => _lazyLogger.Value;

        private static readonly Lazy<IMsgResultStore> _lazyMsgResultStore = ObjectContainer.LazyResolve<IMsgResultStore>();
        private IMsgResultStore _MsgResultStore => _lazyMsgResultStore.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Field(s)  -

        private string _merchantId = "46e02472512f4ed1b388665fa2a1ea7c";
        private string _warehouseId = "fa709344648a4b16ae7841c3d427d9c5";
        private string _orderNo = "99e7ee2ba9ba435da8a8c28ad55cc77d";
        private IList<InventoryDTO> _inventories = new List<InventoryDTO>
        {
            new InventoryDTO { ItemNo = "ITEM1", Qty = 0 },
            new InventoryDTO { ItemNo = "ITEM2", Qty = 0 },
            new InventoryDTO { ItemNo = "ITEM3", Qty = 0 },
            new InventoryDTO { ItemNo = "ITEM4", Qty = 0 },
            new InventoryDTO { ItemNo = "ITEM5", Qty = 0 },
        };

        #endregion
        // *******************************************************************************************************************************
        #region -  Property(ies)  -

        private string MerchantID
        {
            get
            {
                if (_merchantId.IsMissing())
                {
                    _merchantId = Guid.NewGuid().ToLowerNString();
                }
                return _merchantId;
            }
        }
        private string WarehouseID
        {
            get
            {
                if (_warehouseId.IsMissing())
                {
                    _warehouseId = Guid.NewGuid().ToLowerNString();
                }
                return _warehouseId;
            }
        }
        private string OrderNo
        {
            get
            {
                if (_orderNo.IsMissing())
                {
                    _orderNo = Guid.NewGuid().ToLowerNString();
                }
                return _orderNo;
            }
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  AllocateInventories  -

        [Test]
        public async Task AllocateInventories()
        {
            var items = _inventories.DeepClone();
            foreach (var item in items)
            {
                item.Qty = 110;
                item.SafeQty = 100;
            }

            var cmd = new
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                Inventories = items
            };

            var correlationId = Guid.NewGuid();
            var hr = await _InventoryApi.AllocateInventoriesAsync(cmd, correlationId).ConfigureAwait(false);
            var msgCode = await hr.GetMsgCodeAsync().ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);

            var mr = await _MsgResultStore.WaitForResultAsync<HandleResult>(correlationId).ConfigureAwait(false);
            Assert.IsTrue(mr.Result.IsSuccess(), mr.Result);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  HoldOrderInventories  -

        [Test, Order(10)]
        public async Task HoldOrderInventories()
        {
            var items = _inventories.DeepClone();
            foreach (var item in items)
            {
                item.Qty = 100;
            }

            var cmd = new
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                OrderNo = OrderNo,
                Inventories = items
            };

            var correlationId = Guid.NewGuid();
            var hr = await _InventoryApi.HoldOrderInventoriesAsync(cmd, correlationId).ConfigureAwait(false);
            var msgCode = await hr.GetMsgCodeAsync().ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);

            var mr = await _MsgResultStore.WaitForResultAsync<HandleResult>(correlationId).ConfigureAwait(false);
            Assert.IsTrue(mr.Result.IsSuccess(), mr.Result);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  InventoryShipConfirm  -

        [Test, Order(20)]
        public async Task InventoryShipConfirm()
        {
            var items = _inventories.DeepClone();
            foreach (var item in items)
            {
                item.Qty = 100;
            }

            var cmd = new
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                OrderNo = OrderNo,
                Inventories = items
            };

            var correlationId = Guid.NewGuid();
            var hr = await _InventoryApi.InventoryShipConfirmAsync(cmd, correlationId).ConfigureAwait(false);
            var msgCode = await hr.GetMsgCodeAsync().ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);

            var mr = await _MsgResultStore.WaitForResultAsync<HandleResult>(correlationId).ConfigureAwait(false);
            Assert.IsTrue(mr.Result.IsSuccess(), mr.Result);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UnholdOrderInventories  -

        [Test, Order(30)]
        public async Task UnholdOrderInventories()
        {
            var items = _inventories.DeepClone();

            var cmd = new
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                OrderNo = OrderNo,
                Inventories = items
            };

            var correlationId = Guid.NewGuid();
            var hr = await _InventoryApi.UnholdOrderInventoriesAsync(cmd, correlationId).ConfigureAwait(false);
            var msgCode = await hr.GetMsgCodeAsync().ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);

            var mr = await _MsgResultStore.WaitForResultAsync<HandleResult>(correlationId).ConfigureAwait(false);
            Assert.IsTrue(mr.Result.IsSuccess(), mr.Result);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  GetAvailableInventory  -

        [Test]
        public async Task GetAvailableInventory()
        {
            var merchantId = "46e02472512f4ed1b388665fa2a1ea7c";

            var a = await _InventoryApi.GetAvailableInventoryAsync(merchantId, "ITEM1").ConfigureAwait(false);
            var msgCode = await a.GetMsgCodeAsync().ConfigureAwait(false);

            _Logger.Debug("{0}", a);

            Assert.IsTrue(a.IsSuccess, msgCode);
        }

        #endregion
    }
}
