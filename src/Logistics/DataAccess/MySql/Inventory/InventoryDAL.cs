using Dapper;
using SyncSoft.App.Components;
using SyncSoft.App.Utilities;
using SyncSoft.Future.Logistics.Command.Inventory;
using SyncSoft.Future.Logistics.DataAccess.Inventory;
using SyncSoft.Future.Logistics.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.MySql.Inventory
{
    public class InventoryDAL : LogisticsDAL, IInventoryMasterDAL
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IStringHelper> _lazyStringHelper = ObjectContainer.LazyResolve<IStringHelper>();
        private IStringHelper _StringHelper => _lazyStringHelper.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Constructor(s)  -

        public InventoryDAL(ILogisticsDB db) : base(db)
        {
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  AllocateInventoriesAsync  -

        public Task<IList<InventoryDTO>> AllocateInventoriesAsync(AllocateInventoriesCommand cmd)
            => CallInventoriesOperationSPAsync(cmd.CorrelationId
                , cmd.Merchant_ID
                , "INVSP_AllocateInventory"
                , () =>
                {
                    var parameters = cmd.Inventories.Select(x =>
                    {
                        var para = new DynamicParameters();

                        para.Add("Merchant_ID", cmd.Merchant_ID, DbType.String);
                        para.Add("Warehouse_ID", cmd.Warehouse_ID, DbType.String);
                        para.Add("ItemNo", x.ItemNo, DbType.String);
                        para.Add("Qty", x.Qty, DbType.Int32);
                        para.Add("SafeQty", x.SafeQty, DbType.Int32);
                        para.Add("AvailableQty", 0, DbType.Int32, ParameterDirection.Output);

                        return para;
                    }).ToArray();

                    return parameters;
                });

        #endregion
        // *******************************************************************************************************************************
        #region -  HoldOrderInventories  -

        public Task<IList<InventoryDTO>> HoldOrderInventoriesAsync(HoldOrderInventoriesCommand cmd)
        {
            var utcNow = DateTime.UtcNow;

            return CallInventoriesOperationSPAsync(cmd.CorrelationId
                , cmd.Merchant_ID
                , "INVSP_HoldOrderInventory"
                , () =>
                {
                    var parameters = cmd.Inventories.Select(x =>
                    {
                        var para = new DynamicParameters();

                        para.Add("Merchant_ID", cmd.Merchant_ID, DbType.String);
                        para.Add("Warehouse_ID", cmd.Warehouse_ID, DbType.String);
                        para.Add("ItemNo", x.ItemNo, DbType.String);
                        para.Add("OrderNo", cmd.OrderNo, DbType.String);
                        para.Add("Qty", x.Qty, DbType.Int32);
                        para.Add("CreatedOnUtc", utcNow, DbType.DateTime);
                        para.Add("AvailableQty", 0, DbType.Int32, ParameterDirection.Output);

                        return para;
                    }).ToArray();

                    return parameters;
                });
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UnholdOrderInventories  -

        public Task<IList<InventoryDTO>> UnholdOrderInventoriesAsync(UnholdOrderInventoriesCommand cmd)
            => CallInventoriesOperationSPAsync(cmd.CorrelationId
                , cmd.Merchant_ID
                , "INVSP_UnholdOrderInventory"
                , () =>
                {
                    var parameters = cmd.Inventories.Select(x =>
                    {
                        var para = new DynamicParameters();

                        para.Add("Merchant_ID", cmd.Merchant_ID, DbType.String);
                        para.Add("Warehouse_ID", cmd.Warehouse_ID, DbType.String);
                        para.Add("ItemNo", x.ItemNo, DbType.String);
                        para.Add("OrderNo", cmd.OrderNo, DbType.String);
                        para.Add("AvailableQty", 0, DbType.Int32, ParameterDirection.Output);

                        return para;
                    }).ToArray();

                    return parameters;
                });

        #endregion
        // *******************************************************************************************************************************
        #region -  InventoryShipConfirm  -

        public Task<IList<InventoryDTO>> InventoryShipConfirmAsync(InventoryShipConfirmCommand cmd)
            => CallInventoriesOperationSPAsync(cmd.CorrelationId
                , cmd.Merchant_ID
                , "INVSP_InventoryShipConfirm"
                , () =>
                {
                    var parameters = cmd.Inventories.Select(x =>
                    {
                        var para = new DynamicParameters();

                        para.Add("Merchant_ID", cmd.Merchant_ID, DbType.String);
                        para.Add("Warehouse_ID", cmd.Warehouse_ID, DbType.String);
                        para.Add("ItemNo", x.ItemNo, DbType.String);
                        para.Add("OrderNo", cmd.OrderNo, DbType.String);
                        para.Add("Qty", x.Qty, DbType.Int32);
                        para.Add("AvailableQty", 0, DbType.Int32, ParameterDirection.Output);

                        return para;
                    }).ToArray();

                    return parameters;
                });

        #endregion
        // *******************************************************************************************************************************
        #region -  InventoryShipCancel  -

        public Task<IList<InventoryDTO>> InventoryShipCancelAsync(InventoryShipCancelCommand cmd)
            => CallInventoriesOperationSPAsync(cmd.CorrelationId
                , cmd.Merchant_ID
                , "INVSP_InventoryShipCancel"
                , () =>
                {
                    var parameters = cmd.Inventories.Select(x =>
                    {
                        var para = new DynamicParameters();

                        para.Add("Merchant_ID", cmd.Merchant_ID, DbType.String);
                        para.Add("Warehouse_ID", cmd.Warehouse_ID, DbType.String);
                        para.Add("ItemNo", x.ItemNo, DbType.String);
                        para.Add("OrderNo", cmd.OrderNo, DbType.String);
                        para.Add("Qty", x.Qty, DbType.Int32);
                        para.Add("AvailableQty", 0, DbType.Int32, ParameterDirection.Output);

                        return para;
                    }).ToArray();

                    return parameters;
                });

        #endregion
        // *******************************************************************************************************************************
        #region -  CallInventoriesOperationSP  -

        private async Task<IList<InventoryDTO>> CallInventoriesOperationSPAsync(Guid correlationId
            , string merchantId
            , string sql
            , Func<DynamicParameters[]> createParameterFunc
        )
        {
            var parameters = createParameterFunc();

            using (var conn = await CreateConn(merchantId).ConfigureAwait(false))
            using (var tran = conn.BeginTransaction())
            {
                var commandDefinition = new CommandDefinition(commandText: sql
                    , parameters: parameters
                    , transaction: tran
                    , commandType: CommandType.StoredProcedure
                );

                try
                {
                    await conn.ExecuteAsync(commandDefinition).ConfigureAwait(false);

                    tran.Commit();

                    var rs = parameters.Select(x =>
                    {
                        var availableQty = x.Get<int?>("AvailableQty");

                        return new InventoryDTO
                        {
                            ItemNo = x.Get<string>("ItemNo"),
                            Qty = availableQty.GetValueOrDefault()
                        };
                    }).ToArray();

                    return rs;
                }
                catch (DbException ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  ClearOrderHeldInventoriesAsync  -

        public Task<string> ClearOrderHeldInventoriesAsync()
            => base.TryExecuteAsync("INVSP_ClearOrderHeldInventories", commandType: CommandType.StoredProcedure);

        #endregion
        // *******************************************************************************************************************************
        #region -  GetInventories  -

        /// <summary>
        /// 从库存表读取数据
        /// </summary>
        public async Task<IList<InventoryDTO>> GetInventoriesAsync(string merchantId, IEnumerable<string> itemNos)
        {
            using (var conn = await CreateConn(merchantId).ConfigureAwait(false))
            {
                return await conn.QueryListAsync<InventoryDTO>("SELECT ItemNo, Qty, SafeQty FROM Inventories WHERE Merchant_ID = @Merchant_ID AND ItemNo IN @ItemNos",
                    new
                    {
                        Merchant_ID = merchantId,
                        ItemNos = itemNos
                    }).ConfigureAwait(false);
            }
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  GetAvailableInventories  -

        /// <summary>
        /// 计算并获取Item的可用库存
        /// </summary>
        public async Task<IDictionary<string, int>> GetAvailableInventoriesAsync(string merchantId, IEnumerable<string> itemNos)
        {
            var itemNoQueryBuilder = new StringBuilder("'");
            foreach (var itemNo in itemNos)
            {
                itemNoQueryBuilder.Append(_StringHelper.SafeSql(itemNo));
                itemNoQueryBuilder.Append("','");
            }
            itemNoQueryBuilder.Remove(itemNoQueryBuilder.Length - 2, 2);

            using (var conn = await CreateConn(merchantId).ConfigureAwait(false))
            {
                var list = await conn.QueryListAsync<KeyValuePair<string, int>>("INVSP_GetAvailableInventories"
                    , new
                    {
                        Merchant_ID = _StringHelper.SafeSql(merchantId),
                        ItemNos = itemNoQueryBuilder.ToString()
                    }
                    , commandType: CommandType.StoredProcedure
                ).ConfigureAwait(false);

                if (null != list)
                {
                    var rs = list;
                    var dic = rs.ToDictionary(x => x.Key, x => x.Value);
                    if (dic.Count != itemNos.Count())
                    {
                        // 得出差集，补齐结果
                        var exceptedUpcs = itemNos.Except(rs.Select(x => x.Key));
                        foreach (var excpedUpc in exceptedUpcs)
                        {
                            dic.Add(excpedUpc, 0);
                        }
                    }

                    return dic;
                }
                else
                {
                    return itemNos.ToDictionary(x => x, x => 0);
                }
            }
        }

        #endregion
    }
}
