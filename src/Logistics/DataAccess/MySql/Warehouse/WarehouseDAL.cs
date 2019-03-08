using Dapper;
using SyncSoft.App;
using SyncSoft.Future.Logistics.DataAccess.Warehouse;
using SyncSoft.Future.Logistics.DTO.Warehouse;
using SyncSoft.Future.Logistics.Query.Warehouse;
using System;
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
            using (var conn = await CreateConn(dto.Merchant_ID).ConfigureAwait(false))
            {
                return await conn.TryExecuteAsync("INSERT INTO Warehouses(ID, Merchant_ID, Name) VALUES(@ID, @Merchant_ID, @Name)"
                    , dto).ConfigureAwait(false);
            }
        }

        public async Task<MerchantWarehouseDTO> GetAsync(string merchantId, string warehouseId)
        {
            using (var conn = await CreateConn(merchantId).ConfigureAwait(false))
            {
                var mr = await conn.TryQueryFirstOrDefaultAsync<MerchantWarehouseDTO>("SELECT * FROM Warehouses WHERE ID = @id LIMIT 1"
                    , new { ID = warehouseId }).ConfigureAwait(false);
                return mr.Result;
            }
        }

        public async Task<string> UpdateAsync(MerchantWarehouseDTO dto)
        {
            using (var conn = await CreateConn(dto.Merchant_ID).ConfigureAwait(false))
            {
                return await conn.TryExecuteAsync("UPDATE Warehouses SET Name = @Name WHERE ID = @ID"
                    , dto).ConfigureAwait(false);
            }
        }

        public async Task<string> DeleteAsync(MerchantWarehouseDTO dto)
        {
            using (var conn = await CreateConn(dto.Merchant_ID).ConfigureAwait(false))
            {
                return await conn.TryExecuteAsync("DELETE FROM Warehouses WHERE ID = @ID", dto).ConfigureAwait(false);
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

            using (var conn = await CreateConn(query.Merchant_ID).ConfigureAwait(false))
            {
                var mr = await conn.TryExecuteScalarAsync<int>(sql.ToString(), query).ConfigureAwait(false);

                return mr;
            }
        }

        #endregion
    }
}
