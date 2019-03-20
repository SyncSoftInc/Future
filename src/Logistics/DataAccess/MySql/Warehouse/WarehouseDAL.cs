using Dapper;
using SyncSoft.App;
using SyncSoft.Future.Logistics.DataAccess.Warehouse;
using SyncSoft.Future.Logistics.DTO.Warehouse;
using SyncSoft.Future.Logistics.Query.Warehouse;
using System;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.MySql.Warehouse
{
    public class WarehouseDAL : LogisticsDAL, IWarehouseDAL
    {
        public WarehouseDAL(ILogisticsDB db) : base(db)
        {
        }

        // *******************************************************************************************************************************
        #region -  CURD  -

        public async Task<string> InsertAsync(MerchantWarehouseDTO dto)
        {
            var sql = "INSERT INTO Warehouses(ID, Merchant_ID, Name) VALUES(@ID, @Merchant_ID, @Name)";
            try
            {
                using (var conn = await CreateConn(dto.Merchant_ID).ConfigureAwait(false))
                {
                    await conn.ExecuteAsync(sql, parameters: dto).ConfigureAwait(false);
                    return MsgCodes.SUCCESS;
                }
            }
            catch (DbException ex)
            {
                return base.HandleException(null, ex, sql, dto);
            }
        }

        public async Task<MerchantWarehouseDTO> GetAsync(string merchantId, string warehouseId)
        {
            var sql = "SELECT * FROM Warehouses WHERE ID = @id LIMIT 1";
            try
            {
                using (var conn = await CreateConn(merchantId).ConfigureAwait(false))
                {
                    return await conn.QueryFirstOrDefaultAsync<MerchantWarehouseDTO>(sql, parameters: new { ID = warehouseId }).ConfigureAwait(false);
                }
            }
            catch (DbException ex)
            {
                var rs = base.HandleException<MerchantWarehouseDTO>(null, ex, sql, null);
                return rs.Result;
            }
        }

        public async Task<string> UpdateAsync(MerchantWarehouseDTO dto)
        {
            var sql = "UPDATE Warehouses SET Name = @Name WHERE ID = @ID";
            try
            {
                using (var conn = await CreateConn(dto.Merchant_ID).ConfigureAwait(false))
                {
                    await conn.ExecuteAsync(sql, parameters: dto).ConfigureAwait(false);
                    return MsgCODES.SUCCESS;
                }
            }
            catch (DbException ex)
            {
                return base.HandleException(null, ex, sql, dto);
            }
        }

        public async Task<string> DeleteAsync(MerchantWarehouseDTO dto)
        {
            var sql = "DELETE FROM Warehouses WHERE ID = @ID";
            try
            {
                using (var conn = await CreateConn(dto.Merchant_ID).ConfigureAwait(false))
                {
                    await conn.ExecuteAsync(sql, parameters: dto).ConfigureAwait(false);
                    return MsgCODES.SUCCESS;
                }
            }
            catch (DbException ex)
            {
                return base.HandleException(null, ex, sql, dto);
            }
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  Count  -

        public async Task<MsgResult<int>> CountWarehouseAsync(CountWarehouseQuery query)
        {
            var sql = new StringBuilder("SELECT COUNT(0) FROM Warehouses WHERE Merchant_ID = @Merchant_ID");

            if (query.Name.IsPresent())
            {
                sql.Append(" AND Name = @Name");
            }

            if (query.Warehouse_ID.IsPresent())
            {
                sql.Append(" AND ID <> @Warehouse_ID");
            }

            try
            {
                using (var conn = await CreateConn(query.Merchant_ID).ConfigureAwait(false))
                {
                    var rs = await conn.ExecuteScalarAsync<int>(sql.ToString(), parameters: query).ConfigureAwait(false);
                    return new MsgResult<int>(MsgCodes.SUCCESS, rs);
                }
            }
            catch (DbException ex)
            {
                return base.HandleException<int>(null, ex, sql.ToString(), query);
            }
        }

        #endregion
    }
}
