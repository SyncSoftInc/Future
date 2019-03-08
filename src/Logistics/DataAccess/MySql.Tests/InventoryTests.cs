using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyncSoft.App.Components;
using SyncSoft.Show.Warehouse.Command;
using SyncSoft.Show.Warehouse.DataAccess;
using SyncSoft.Show.Warehouse.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SyncSoft.Show.Warehouse.MySql.Tests
{
    [TestClass]
    public class InventoryTests
    {
        private static readonly Lazy<IInventoryDAL> _lazyInventoryDAL = ObjectContainer.LazyResolve<IInventoryDAL>();
        private IInventoryDAL _InventoryDAL => _lazyInventoryDAL.Value;

        [TestMethod]
        public void InventoryLifeCycle()
        {
            var max = 100;
            var dic = new Dictionary<string, int>(max);
            for (int i = 0; i < max; i++)
            {
                dic.Add(i.ToString("D10"), i);
            }

            _InventoryDAL.BatchInsertAsync(new BachInsertInventoriesCommand
            {
                Merchant_ID = TestConstants.Merchant_ID,
                Warehouse_ID = TestConstants.Warehouse_ID,
                Inventories = dic
            }).Execute();

            var sampleUpc = 37.ToString("D10");

            var invQty = _InventoryDAL.GetInvQtyAsync(new GetInvQtyQuery
            {
                Merchant_ID = TestConstants.Merchant_ID,
                Warehouse_ID = TestConstants.Warehouse_ID,
                UPC = sampleUpc
            }).Execute();

            Assert.AreEqual(invQty, 37);

            for (int i = 0; i < max; i++)
            {
                dic[i.ToString("D10")] = 100 - i;
            }

            _InventoryDAL.BatchInsertAsync(new BachInsertInventoriesCommand
            {
                Merchant_ID = TestConstants.Merchant_ID,
                Warehouse_ID = TestConstants.Warehouse_ID,
                Inventories = dic
            }).Execute();

            invQty = _InventoryDAL.GetInvQtyAsync(new GetInvQtyQuery
            {
                Merchant_ID = TestConstants.Merchant_ID,
                Warehouse_ID = TestConstants.Warehouse_ID,
                UPC = sampleUpc
            }).Execute();

            Assert.AreEqual(invQty, 63);

            for (int i = 0; i < max; i++)
            {
                dic[i.ToString("D10")] = new Random(i).Next(0, 100);
            }
            _InventoryDAL.BatchUpdateAsync(new BachUpdateInventoriesCommand
            {
                Merchant_ID = TestConstants.Merchant_ID,
                Warehouse_ID = TestConstants.Warehouse_ID,
                Inventories = dic
            }).Execute();

            invQty = _InventoryDAL.GetInvQtyAsync(new GetInvQtyQuery
            {
                Merchant_ID = TestConstants.Merchant_ID,
                Warehouse_ID = TestConstants.Warehouse_ID,
                UPC = sampleUpc
            }).Execute();

            Trace.WriteLine(invQty);

            _InventoryDAL.BatchDeleteAsync(new BachDeleteInventoriesCommand
            {
                Merchant_ID = TestConstants.Merchant_ID,
                Warehouse_ID = TestConstants.Warehouse_ID,
                Inventories = dic.Keys.ToList()
            }).Execute();
        }
    }
}
