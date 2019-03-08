using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.App.Messaging;
using SyncSoft.Future.Logistics.API.Inventory;
using SyncSoft.Future.Logistics.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.IntegratedTest.Inventory
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
            new InventoryDTO { UPC = "ITEM1", Qty = 0 },
            new InventoryDTO { UPC = "ITEM2", Qty = 0 },
            new InventoryDTO { UPC = "ITEM3", Qty = 0 },
            new InventoryDTO { UPC = "ITEM4", Qty = 0 },
            new InventoryDTO { UPC = "ITEM5", Qty = 0 },
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

        [Test]
        public void AllocateInventories()
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
            _InventoryApi.AllocateInventoriesAsync(cmd, correlationId).ResultForTest();

            var mr = _MsgResultStore.WaitAsync<string>(correlationId).Execute();
            Assert.IsTrue(mr.IsSuccess, mr.MsgCode);
        }
        [Test]
        public void GetAvailableInventory()
        {
            var merchantId = "46e02472512f4ed1b388665fa2a1ea7c";

            var a = _InventoryApi.GetAvailableInventoryAsync(merchantId, "ITEM1").Execute();

            _Logger.Debug("{0}", a);

            Assert.IsTrue(a.IsSuccess, a.GetMsgCodeAsync().Execute());
        }
    }
}
