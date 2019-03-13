using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using SyncSoft.App.DataAccess;
using SyncSoft.ECP.DTOs.Users;

namespace SyncSoft.Future.Passport.MySql
{
    public class UserDAL : ECP.MySql.ECPMySqlDAL, IUserDAL
    {
        // *******************************************************************************************************************************
        #region -  Constructor(s)  -

        public UserDAL(IPassportDB db) : base(db)
        {
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  CRUD  -

        public Task<string> InsertUserAsync(UserBasicInfoDTO dto) => base.TryExecuteAsync("PASSP_InsertUser", dto, commandType: CommandType.StoredProcedure);

        public Task<string> UpdateUserAsync(UserBasicInfoDTO dto) => base.TryExecuteAsync("PASSP_UpdateUser", dto, commandType: CommandType.StoredProcedure);

        public Task<string> DeleteUserAsync(Guid id) => base.TryExecuteAsync("PASSP_DeleteUser", new { ID = id }, commandType: CommandType.StoredProcedure);

        #endregion
    }
}
