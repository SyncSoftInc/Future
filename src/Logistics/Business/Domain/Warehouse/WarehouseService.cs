using SyncSoft.App.Components;
using SyncSoft.Future.Logistics.Command.Warehouse;
using SyncSoft.Future.Logistics.DataAccess.Warehouse;
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
        private IWarehouseIdFactory _WarehouseIdFactory => _lazyWarehouseIdFactory.Value;

        private static readonly Lazy<IWarehouseDAL> _lazyWarehouseDAL = ObjectContainer.LazyResolve<IWarehouseDAL>();
        private IWarehouseDAL _WarehouseDAL => _lazyWarehouseDAL.Value;

        private static readonly Lazy<IMerchantSettingProvider> _lazyMerchantSettingProvider = ObjectContainer.LazyResolve<IMerchantSettingProvider>();
        private IMerchantSettingProvider _MerchantSettingProvider => _lazyMerchantSettingProvider.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Create  -

        public async Task<string> CreateAsync(CreateWarehouseCommand cmd)
        {
            var dto = new MerchantWarehouseDTO
            {
                ID = cmd.ID,
                Merchant_ID = cmd.Merchant_ID,
                Name = cmd.Name
            };

            var countQuery = new CountWarehouseQuery { Merchant_ID = dto.Merchant_ID };

            // 允许的仓库数量判断
            var countMr = await _WarehouseDAL.CountWarehouseAsync(countQuery).ConfigureAwait(false);
            if (!countMr.IsSuccess) return countMr.MsgCode;
            // ^^^^^^^^^^ 查询失败

            // 获取商家设置信息
            var merchantSetting = await _MerchantSettingProvider.GetAsync(dto.Merchant_ID).ConfigureAwait(false);
            if (countMr.Result >= merchantSetting.MaxWarehouseLimit) return MsgCodes.WH_0000000001;
            // ^^^^^^^^^^ 达到或超过允许的数量

            // 名称重复判断
            countQuery.Name = cmd.Name;
            countMr = await _WarehouseDAL.CountWarehouseAsync(countQuery).ConfigureAwait(false);
            if (!countMr.IsSuccess) return countMr.MsgCode;
            // ^^^^^^^^^^ 查询失败
            if (countMr.Result > 0) return MsgCodes.WH_0000000002;
            // ^^^^^^^^^^ 名称重复

            // 创建ID
            if (dto.ID.IsMissing())
            {
                dto.ID = await _WarehouseIdFactory.CreateNewAsync().ConfigureAwait(false);
            }
            // 插入数据库
            return await _WarehouseDAL.InsertAsync(dto).ConfigureAwait(false);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  Update  -

        public async Task<string> UpdateAsync(UpdateWarehouseCommand cmd)
        {
            // 名称重复判断
            var countQuery = new CountWarehouseQuery
            {
                Merchant_ID = cmd.Merchant_ID,
                Warehouse_ID = cmd.Warehouse_ID,
                Name = cmd.Name
            };
            var countMr = await _WarehouseDAL.CountWarehouseAsync(countQuery).ConfigureAwait(false);
            if (!countMr.IsSuccess) return countMr.MsgCode;
            // ^^^^^^^^^^ 查询失败
            if (countMr.Result > 0) return MsgCodes.WH_0000000002;
            // ^^^^^^^^^^ 名称重复

            // 插入数据库
            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = cmd.Merchant_ID,
                ID = cmd.Warehouse_ID,
                Name = cmd.Name
            };
            return await _WarehouseDAL.UpdateAsync(dto).ConfigureAwait(false);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  Delete  -

        public async Task<string> DeleteAsync(DeleteWarehouseCommand cmd)
        {
            var dto = new MerchantWarehouseDTO
            {
                Merchant_ID = cmd.Merchant_ID,
                ID = cmd.Warehouse_ID,
            };
            return await _WarehouseDAL.DeleteAsync(dto).ConfigureAwait(false);
        }

        #endregion
    }
}
