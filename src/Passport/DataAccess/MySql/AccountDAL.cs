using System;
using System.Data;
using System.Threading.Tasks;
using SyncSoft.App;
using SyncSoft.ECP.DTOs.Account;

namespace SyncSoft.Future.Passport.MySql
{
    public class AccountDAL : SyncSoft.ECP.MySql.Account.AccountDAL
    {
        // *******************************************************************************************************************************
        #region -  Constructor(s)  -

        public AccountDAL(IPassportDB db) : base(db)
        {
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  CRUD  -

        public async override Task<bool> IsUsernameExistAsync(string username)
        {
            var query = await base.TryExecuteScalarAsync<bool>("PASSP_AccountUsernameExist", new
            {
                Username = username
            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            return query.Result;
        }

        public override Task<string> InsertAccountAsync(AccountDTO dto)
            => base.TryExecuteAsync("PASSP_InsertAccount", dto, commandType: CommandType.StoredProcedure);

        public override Task<string> UpdateUsernameAsync(AccountDTO dto)
            => base.TryExecuteAsync("PASSP_UpdateAccountUsername", dto, commandType: CommandType.StoredProcedure);

        public override Task<string> UpdatePasswordAsync(AccountDTO dto)
            => base.TryExecuteAsync("PASSP_UpdateAccountPassword", dto, commandType: CommandType.StoredProcedure);

        public override Task<string> UpdateAccountStatusAsync(AccountDTO dto)
            => base.TryExecuteAsync("PASSP_UpdateAccountStatus", dto, commandType: CommandType.StoredProcedure);

        public override Task<string> UpdateLoginInfoAsync(AccountDTO dto)
            => base.TryExecuteAsync("PASSP_UpdateAccountLoginInfo", dto, commandType: CommandType.StoredProcedure);

        public override Task<string> DeleteAccountAsync(Guid id)
           => base.TryExecuteAsync("PASSP_DeleteAccount", new { ID = id }, commandType: CommandType.StoredProcedure);

        #endregion
        // *******************************************************************************************************************************
        #region -  Queries  -

        public async override Task<AccountDTO> GetAccountAsync(Guid id)
        {
            var query = await base.TryQueryFirstOrDefaultAsync<AccountDTO>("PASSP_GetAccountByID", new
            {
                ID = id
            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            return query.Result;
        }

        public async override Task<AccountDTO> GetAccountAsync(string username)
        {
            var query = await base.TryQueryFirstOrDefaultAsync<AccountDTO>("PASSP_GetAccountByUsername", new
            {
                Username = username
            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            return query.Result;
        }

        #endregion
    }
}
