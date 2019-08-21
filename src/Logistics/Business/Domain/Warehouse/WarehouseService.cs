using SyncSoft.App.Components;
using SyncSoft.Future.Logistics.Command.Warehouse;
using SyncSoft.Future.Logistics.DataAccess;
using SyncSoft.Future.Logistics.DTO.Warehouse;
using SyncSoft.Future.Logistics.Query.Warehouse;
using SyncSoft.Future.Setting;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Domain.Warehouse
{
    public class WarehouseService : IWarehouseService
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IWarehouseIdFactory> _lazyWarehouseIdFactory = ObjectContainer.LazyResolve<IWarehouseIdFactory>();
        private IWarehouseIdFactory WarehouseIdFactory => _lazyWarehouseIdFactory.Value;

        private static readonly Lazy<IMerchantSettingProvider> _lazyMerchantSettingProvider = ObjectContainer.LazyResolve<IMerchantSettingProvider>();
        private IMerchantSettingProvider MerchantSettingProvider => _lazyMerchantSettingProvider.Value;

        private static readonly Lazy<ILogisticsMasterDALFactory> _lazyLogisticsMasterDALFactory = ObjectContainer.LazyResolve<ILogisticsMasterDALFactory>();
        private ILogisticsMasterDALFactory LogisticsMasterDALFactory => _lazyLogisticsMasterDALFactory.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Create  -

        public async Task<string> CreateAsync(CreateWarehouseCommand cmd)
        {
            // 创建数据访问层
            var warehouseDAL = await LogisticsMasterDALFactory.CreateWarehouseDALAsync(cmd.Merchant_ID).ConfigureAwait(false);

            var dto = new MerchantWarehouseDTO
            {
                ID = cmd.ID,
                Merchant_ID = cmd.Merchant_ID,
                Name = cmd.Name
            };

            // 允许的仓库数量判断
            var countQuery = new CountWarehouseQuery { Merchant_ID = dto.Merchant_ID };
            var count = await warehouseDAL.CountWarehouseAsync(countQuery).ConfigureAwait(false);

            // 获取商家设置信息
            var merchantSetting = await MerchantSettingProvider.GetAsync(dto.Merchant_ID).ConfigureAwait(false);
            if (count >= merchantSetting.MaxWarehouseLimit) return MsgCodes.WH_0000000001;
            // ^^^^^^^^^^ 达到或超过允许的数量

            // 名称重复判断
            countQuery.Name = cmd.Name;
            count = await warehouseDAL.CountWarehouseAsync(countQuery).ConfigureAwait(false);
            if (count > 0) return MsgCodes.WH_0000000002;
            // ^^^^^^^^^^ 名称重复

            // 创建ID
            if (dto.ID.IsMissing())
            {
                dto.ID = await WarehouseIdFactory.CreateNewAsync().ConfigureAwait(false);
            }
            // 插入数据库
            await warehouseDAL.InsertAsync(dto).ConfigureAwait(false);

            return MsgCodes.SUCCESS;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  Update  -

        public async Task<string> UpdateAsync(UpdateWarehouseCommand cmd)
        {
            // 创建数据访问层
            var warehouseDAL = await LogisticsMasterDALFactory.CreateWarehouseDALAsync(cmd.Merchant_ID).ConfigureAwait(false);

            // 名称重复判断
            var countQuery = new CountWarehouseQuery
            {
                Merchant_ID = cmd.Merchant_ID,
                Warehouse_ID = cmd.Warehouse_ID,
                Name = cmd.Name
            };
            var count = await warehouseDAL.CountWarehouseAsync(countQuery).ConfigureAwait(false);
            if (count > 0) return MsgCodes.WH_0000000002;
            // ^^^^^^^^^^ 名称重复

            // 更新数据库
            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = cmd.Merchant_ID,
                ID = cmd.Warehouse_ID,
                Name = cmd.Name
            };
            return await warehouseDAL.UpdateAsync(dto).ConfigureAwait(false);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  Delete  -

        public async Task<string> DeleteAsync(DeleteWarehouseCommand cmd)
        {
            // 创建数据访问层
            var warehouseDAL = await LogisticsMasterDALFactory.CreateWarehouseDALAsync(cmd.Merchant_ID).ConfigureAwait(false);

            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = cmd.Merchant_ID,
                ID = cmd.Warehouse_ID,
            };
            return await warehouseDAL.DeleteAsync(dto).ConfigureAwait(false);
        }

        #endregion
    }
}
