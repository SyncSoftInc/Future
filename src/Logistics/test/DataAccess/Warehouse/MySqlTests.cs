using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.Future.Logistics.DataAccess;
using SyncSoft.Future.Logistics.DTO.Warehouse;
using System;
using System.Threading.Tasks;

namespace DataAccessTest.Warehouse
{
    public class MySqlTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<ILogisticsMasterDALFactory> _lazyLogisticsMasterDALFactory = ObjectContainer.LazyResolve<ILogisticsMasterDALFactory>();
        private ILogisticsMasterDALFactory LogisticsMasterDALFactory => _lazyLogisticsMasterDALFactory.Value;

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
        public async Task Insert()
        {
            var warehouseDAL = await LogisticsMasterDALFactory.CreateWarehouseDALAsync(MerchantID).ConfigureAwait(false);

            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = MerchantID,
                ID = WarehouseID,
                Name = $"Warehouse {WarehouseID}"
            };

            var msgCode = await warehouseDAL.InsertAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(1)]
        public async Task GetSingle()
        {
            var warehouseDAL = await LogisticsMasterDALFactory.CreateWarehouseDALAsync(MerchantID).ConfigureAwait(false);
            var dto = await warehouseDAL.GetAsync(MerchantID, WarehouseID).ConfigureAwait(false);
            Assert.IsNotNull(dto);
        }

        [Test, Order(2)]
        public async Task Update()
        {
            var warehouseDAL = await LogisticsMasterDALFactory.CreateWarehouseDALAsync(MerchantID).ConfigureAwait(false);
            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = MerchantID,
                ID = WarehouseID,
                Name = "Warehouse.updated"
            };

            var msgCode = await warehouseDAL.UpdateAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }

        [Test, Order(3)]
        public async Task Delete()
        {
            var warehouseDAL = await LogisticsMasterDALFactory.CreateWarehouseDALAsync(MerchantID).ConfigureAwait(false);
            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = MerchantID,
                ID = WarehouseID,
            };
            var msgCode = await warehouseDAL.DeleteAsync(dto).ConfigureAwait(false);
            Assert.IsTrue(msgCode.IsSuccess(), msgCode);
        }
    }
}