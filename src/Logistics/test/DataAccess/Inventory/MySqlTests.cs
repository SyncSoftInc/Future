using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncSoft.Future.Warehouse.DataAccessTest.Inventory
{
    public class MySqlTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IInventoryMasterDAL> _lazyInventoryMasterDAL = ObjectContainer.LazyResolve<IInventoryMasterDAL>();
        private IInventoryMasterDAL _InventoryMasterDAL => _lazyInventoryMasterDAL.Value;

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger(nameof(MySqlTests));
        private ILogger _Logger => _lazyLogger.Value;

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

            var a = _InventoryMasterDAL.AllocateInventoriesAsync(cmd).Execute();
            _Logger.Debug("{@0}", a);
            Assert.IsTrue(a.Count > 0);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  HoldInventories  -

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

            var a = _InventoryMasterDAL.HoldOrderInventoriesAsync(cmd).Execute();
            _Logger.Debug("{@0}", a);
            Assert.IsTrue(a.Count > 0);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  InventoryShipConfirm  -

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

            var a = _InventoryMasterDAL.InventoryShipConfirmAsync(cmd).Execute();
            _Logger.Debug("{@0}", a);
            Assert.IsTrue(a.Count > 0);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  InventoryShipCancel  -

        [Test, Order(30)]
        public void InventoryShipCancel()
        {
            var items = _inventories.DeepClone();
            foreach (var item in items)
            {
                item.Qty = 100;
            }

            var cmd = new InventoryShipCancelCommand
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                OrderNo = OrderNo,
                Inventories = items
            };

            var a = _InventoryMasterDAL.InventoryShipCancelAsync(cmd).Execute();
            _Logger.Debug("{@0}", a);
            Assert.IsTrue(a.Count > 0);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UnholdOrderInventories  -

        [Test, Order(40)]
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

            var a = _InventoryMasterDAL.UnholdOrderInventoriesAsync(cmd).Execute();
            _Logger.Debug("{@0}", a);
            Assert.IsTrue(a.Count > 0);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  GetInventories  -

        [Test, Order(100)]
        public void GetInventories()
        {
            var list = _inventories.Select(x => x.ItemNo).ToList();

            var a = _InventoryMasterDAL.GetInventoriesAsync(MerchantID, list.ToArray()).Execute();

            _Logger.Debug("{@a}", a);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  GetAvailableInventories  -

        [Test, Order(110)]
        public void GetAvailableInventories()
        {
            var list = _inventories.Select(x => x.ItemNo).ToList();
            list.Add("ITEM99");

            var a = _InventoryMasterDAL.GetAvailableInventoriesAsync(MerchantID, list.ToArray()).Execute();

            _Logger.Debug("{@a}", a);

            Assert.IsTrue(a["ITEM99"] == 0);
        }

        #endregion

        // *******************************************************************************************************************************
        #region -  GetAvailableInventories  -

        [Test, Order(150)]
        public void ClearOrderHeldInventories()
        {
            var msgCode = _InventoryMasterDAL.ClearOrderHeldInventoriesAsync().Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        #endregion
    }
}