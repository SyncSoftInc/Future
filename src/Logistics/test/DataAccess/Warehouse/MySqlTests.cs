using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.Future.Logistics.DataAccess.Warehouse;
using SyncSoft.Future.Logistics.DTO.Warehouse;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Warehouse.DataAccessTest.Warehouse
{
    public class MySqlTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IWarehouseDAL> _lazyWarehouseDAL = ObjectContainer.LazyResolve<IWarehouseDAL>();
        private IWarehouseDAL _WarehouseDAL => _lazyWarehouseDAL.Value;

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
        public void Insert()
        {
            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = MerchantID,
                ID = WarehouseID,
                Name = $"Warehouse {WarehouseID}"
            };

            var msgCode = _WarehouseDAL.InsertAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(1)]
        public void GetSingle()
        {
            var dto = _WarehouseDAL.GetAsync(MerchantID, WarehouseID).Execute();
            Assert.IsNotNull(dto);
        }

        [Test, Order(2)]
        public void Update()
        {
            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = MerchantID,
                ID = WarehouseID,
                Name = "Warehouse.updated"
            };

            var msgCode = _WarehouseDAL.UpdateAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(3)]
        public void Delete()
        {
            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = MerchantID,
                ID = WarehouseID,
            };
            var msgCode = _WarehouseDAL.DeleteAsync(dto).Execute();
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}