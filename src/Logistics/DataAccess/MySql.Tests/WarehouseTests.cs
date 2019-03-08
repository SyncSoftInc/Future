using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyncSoft.App.Components;
using SyncSoft.Show.Warehouse.DataAccess;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Show.Warehouse.MySql.Tests
{
    [TestClass]
    public class WarehouseTests
    {
        private static readonly Lazy<IWarehouseDAL> _lazyWarehouseDAL = ObjectContainer.LazyResolve<IWarehouseDAL>();
        private IWarehouseDAL _WarehouseDAL => _lazyWarehouseDAL.Value;

        [TestMethod]
        public void WarehouseLifeCycle()
        {
            var dto = new DTO.MerchantWarehouseDTO
            {
                ID = Guid.NewGuid().ToString("n"),
                Merchant_ID = TestConstants.Merchant_ID
            };
            dto.Name = "Warehouse " + dto.ID;
            _WarehouseDAL.InsertAsync(dto).Execute();

            var mr = _WarehouseDAL.GetAsync(dto.Merchant_ID, dto.ID).Execute();
            Assert.IsNotNull(mr);

            _WarehouseDAL.UpdateAsync(dto).Execute();

            _WarehouseDAL.DeleteAsync(dto).Execute();
            mr = _WarehouseDAL.GetAsync(dto.Merchant_ID, dto.ID).Execute();
            Assert.IsNull(mr);
        }
    }
}
