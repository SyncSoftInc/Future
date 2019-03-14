using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.App.Messaging;
using SyncSoft.Future.Logistics.API.Warehouse;
using SyncSoft.Future.Logistics.Domain.Warehouse;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.IntegratedTest.Warehouse
{
    public class WarehouseApiTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IWarehouseApi> _lazyWarehouseApi = ObjectContainer.LazyResolve<IWarehouseApi>();
        private IWarehouseApi _WarehouseApi => _lazyWarehouseApi.Value;

        private static readonly Lazy<IMsgResultStore> _lazyMsgResultStore = ObjectContainer.LazyResolve<IMsgResultStore>();
        private IMsgResultStore _MsgResultStore => _lazyMsgResultStore.Value;

        private static readonly Lazy<IWarehouseIdFactory> _lazyWarehouseIdFactory = ObjectContainer.LazyResolve<IWarehouseIdFactory>();
        private IWarehouseIdFactory _WarehouseIdFactory => _lazyWarehouseIdFactory.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Field(s)  -

        private string _merchantId = "5a6849acc5624c629f6c2307ee6c7cdc";
        private string _warehouseId = "f30af6aea8d64340982ff6a9ff1500a0";

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

        #endregion


        [Test, Order(0)]
        public void Create()
        {
            var cmd = new
            {
                Merchant_ID = MerchantID,
                ID = WarehouseID,
                Name = $"Warehouse {WarehouseID}"
            };

            var correlationId = Guid.NewGuid();

            var msgCode = _WarehouseApi.CreateAsync(cmd, correlationId).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);

            var mr = _MsgResultStore.WaitAsync<string>(correlationId).Execute();
            Assert.IsTrue(mr.IsSuccess, mr.MsgCode);
        }

        [Test, Order(1)]
        public void GetSingle()
        {
            var dto = _WarehouseApi.GetSingleAsync(MerchantID, WarehouseID).ResultForTest();
            Assert.IsNotNull(dto);
        }

        [Test, Order(2)]
        public void Update()
        {
            var cmd = new
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
                Name = "Warehouse.updated"
            };

            var correlationId = Guid.NewGuid();

            var msgCode = _WarehouseApi.UpdateAsync(cmd, correlationId).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);

            var mr = _MsgResultStore.WaitAsync<string>(correlationId).Execute();
            Assert.IsTrue(mr.IsSuccess, mr.MsgCode);
        }

        [Test, Order(3)]
        public void Delete()
        {
            var cmd = new
            {
                Merchant_ID = MerchantID,
                Warehouse_ID = WarehouseID,
            };
            var correlationId = Guid.NewGuid();

            var msgCode = _WarehouseApi.DeleteAsync(cmd, correlationId).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);

            var mr = _MsgResultStore.WaitAsync<string>(correlationId).Execute();
            Assert.IsTrue(mr.IsSuccess, mr.MsgCode);
        }
    }
}
