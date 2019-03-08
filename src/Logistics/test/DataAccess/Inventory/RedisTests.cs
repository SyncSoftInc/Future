using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncSoft.Future.Warehouse.DataAccessTest.Inventory
{
    public class RedisTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IInventoryQueryDAL> _lazyInventorySlaveDAL = ObjectContainer.LazyResolve<IInventoryQueryDAL>();
        private IInventoryQueryDAL _InventorySlaveDAL => _lazyInventorySlaveDAL.Value;

        #endregion

        private Dictionary<string, int> _dic = new Dictionary<string, int> { { "BBB", 1 }
            , { "CCC", 2 }
            , { "DDD", 3 }
            , { "EEE", 4 }
            , { "FFF", 5 }
        };

        [Test, Order(0)]
        public void SyncInventories()
        {
            _InventorySlaveDAL.SyncInventoriesAsync("AAA", _dic.ToKeyValuePairs()).Execute();
        }

        [Test, Order(1)]
        public void GetAvailableInventories()
        {
            var inventories = _InventorySlaveDAL.GetAvailableInventoriesAsync("AAA", _dic.Keys.ToArray()).Execute();

            Assert.AreEqual(inventories["DDD"], _dic["DDD"]);
        }

        [Test, Order(2)]
        public void GetAvailableInventory()
        {
            var qty = _InventorySlaveDAL.GetAvailableInventoryAsync("AAA", "EEE").Execute();

            Assert.AreEqual(qty, _dic["EEE"]);
        }
    }
}
