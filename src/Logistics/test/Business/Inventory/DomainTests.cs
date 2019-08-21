using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.Domain.Inventory;
using SyncSoft.Future.Logistics.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessTest.Inventory
{
    public class DomainTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IInventoryService> _lazyInventoryService = ObjectContainer.LazyResolve<IInventoryService>();
        private IInventoryService _InventoryService => _lazyInventoryService.Value;

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

        [Test, Order(0)]
        public void AllocateInventories()
        {
            var items = _inventories.DeepClone();
            foreach (var item in items)
            {
                item.Qty = 110;
                item.SafeQty = 100;
            }

            var cmd = new AllocateInventoriesCommand
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                Inventories = items
            };

            var msgCode = _InventoryService.AllocateInventoriesAsync(cmd).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  HoldOrderInventories  -

        [Test, Order(10)]
        public void HoldOrderInventories()
        {
            var items = _inventories.DeepClone();
            foreach (var item in items)
            {
                item.Qty = 100;
            }

            var cmd = new HoldOrderInventoriesCommand
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                OrderNo = OrderNo,
                Inventories = items
            };

            var msgCode = _InventoryService.HoldOrderInventoriesAsync(cmd).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  HoldOrderInventories  -

        [Test, Order(20)]
        public void InventoryShipConfirm()
        {
            var items = _inventories.DeepClone();
            foreach (var item in items)
            {
                item.Qty = 100;
            }

            var cmd = new InventoryShipConfirmCommand
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                OrderNo = OrderNo,
                Inventories = items
            };

            var msgCode = _InventoryService.InventoryShipConfirmAsync(cmd).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UnholdInventories  -

        [Test, Order(30)]
        public void UnholdOrderInventories()
        {
            var items = _inventories.DeepClone();

            var cmd = new UnholdOrderInventoriesCommand
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                OrderNo = OrderNo,
                Inventories = items
            };

            var msgCode = _InventoryService.UnholdOrderInventoriesAsync(cmd).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UnholdInventories  -

        [Test, Order(150)]
        public void ClearOrderHeldInventories()
        {
            var msgCode = _InventoryService.ClearOrderHeldInventoriesAsync().Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        #endregion
    }
}