using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.App.Messaging;
using SyncSoft.Future.Logistics.API.Warehouse;
using SyncSoft.Future.Logistics.Domain.Warehouse;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.IntegratedTest
{
    public class WarehouseApiTests
    {
        private static readonly Lazy<IWarehouseApi> _lazyWarehouseApi = ObjectContainer.LazyResolve<IWarehouseApi>();
        private IWarehouseApi _WarehouseApi => _lazyWarehouseApi.Value;

        private static readonly Lazy<IMsgResultStore> _lazyMsgResultStore = ObjectContainer.LazyResolve<IMsgResultStore>();
        private IMsgResultStore _MsgResultStore => _lazyMsgResultStore.Value;

        private static readonly Lazy<IWarehouseIdFactory> _lazyWarehouseIdFactory = ObjectContainer.LazyResolve<IWarehouseIdFactory>();
        private IWarehouseIdFactory _WarehouseIdFactory => _lazyWarehouseIdFactory.Value;

        [Test]
        public void LifeCycle()
        {
            var warehouseId = _WarehouseIdFactory.CreateNewAsync().Execute();
            var createCmd = new
            {
                ID = warehouseId,
                Name = $"Warehouse {warehouseId}",
                Merchant_ID = TestConstants.Merchant_ID
            };

            // Create
            var correlationId = Guid.NewGuid();
            _WarehouseApi.CreateAsync(createCmd, correlationId).ResultForTest();
            var mr = _MsgResultStore.WaitAsync<string>(correlationId).Execute();
            Assert.IsTrue(mr.IsSuccess, mr.MsgCode);

            var dto = _WarehouseApi.GetSingleAsync(createCmd.Merchant_ID, createCmd.ID).ResultForTest();
            Assert.IsNotNull(dto, "dto is null.");

            // Update
            correlationId = Guid.NewGuid();
            _WarehouseApi.UpdateAsync(new
            {
                Warehouse_ID = warehouseId,
                Merchant_ID = TestConstants.Merchant_ID,
                Name = createCmd.Name + ".updated"
            }, correlationId).ResultForTest();
            mr = _MsgResultStore.WaitAsync<string>(correlationId).Execute();
            Assert.IsTrue(mr.IsSuccess, mr.MsgCode);

            dto = _WarehouseApi.GetSingleAsync(createCmd.Merchant_ID, createCmd.ID).ResultForTest();
            Assert.IsTrue(dto.Name.Contains(".updated"), "Name doesn't contain keyword.");

            // Delete
            correlationId = Guid.NewGuid();
            _WarehouseApi.DeleteAsync(new
            {
                Warehouse_ID = warehouseId,
                Merchant_ID = TestConstants.Merchant_ID
            }, correlationId).ResultForTest();
            mr = _MsgResultStore.WaitAsync<string>(correlationId).Execute();
            Assert.IsTrue(mr.IsSuccess, mr.MsgCode);

            dto = _WarehouseApi.GetSingleAsync(createCmd.Merchant_ID, createCmd.ID).ResultForTest();
            Assert.IsNull(dto);
        }
    }
}
