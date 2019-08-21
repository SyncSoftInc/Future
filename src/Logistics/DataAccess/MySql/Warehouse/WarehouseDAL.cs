using SyncSoft.Future.Logistics.DataAccess;
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
        // *******************************************************************************************************************************
        #region -  Constructor(s)  -

        public WarehouseDAL(ILogisticsDB db) : base(db)
        {
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  CURD  -

        public async Task<string> InsertAsync(MerchantWarehouseDTO dto)
        {
            var sql = "INSERT INTO Warehouses(ID, Merchant_ID, Name) VALUES(@ID, @Merchant_ID, @Name)";
            return await base.TryExecuteAsync(sql, dto).ConfigureAwait(false);
        }

        public async Task<MerchantWarehouseDTO> GetAsync(string merchantId, string warehouseId)
        {
            var sql = "SELECT * FROM Warehouses WHERE ID = @id LIMIT 1";
            return await base.QueryFirstOrDefaultAsync<MerchantWarehouseDTO>(sql, new { ID = warehouseId }).ConfigureAwait(false);
        }

        public async Task<string> UpdateAsync(MerchantWarehouseDTO dto)
        {
            var sql = "UPDATE Warehouses SET Name = @Name WHERE ID = @ID";
            return await base.TryExecuteAsync(sql, dto).ConfigureAwait(false);
        }

        public async Task<string> DeleteAsync(MerchantWarehouseDTO dto)
        {
            var sql = "DELETE FROM Warehouses WHERE ID = @ID";
            return await base.TryExecuteAsync(sql, dto).ConfigureAwait(false);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  Count  -

        public async Task<int> CountWarehouseAsync(CountWarehouseQuery query)
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

            return await base.ExecuteScalarAsync<int>(sql.ToString(), query).ConfigureAwait(false);
        }

        #endregion
    }
}
