using SyncSoft.ECP;
using SyncSoft.ECP.Identity;
using SyncSoft.ECP.MySql;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.DAL.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.MySql
{
    public class UserDAL : ECPMySqlDAL, IUserProfileProvider, IUserDAL
    {
        // *******************************************************************************************************************************
        #region -  Constructor(s)  -

        public UserDAL(IPassportDB db) : base(db)
        {
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  CRUD  -

        public Task<string> InsertUserAsync(UserDTO dto) => base.TryExecuteAsync("PASSP_InsertUser", dto, commandType: CommandType.StoredProcedure);

        public Task<string> UpdateUserAsync(UserDTO dto) => base.TryExecuteAsync("PASSP_UpdateUser", dto, commandType: CommandType.StoredProcedure);

        public Task<string> DeleteUserAsync(Guid id) => base.TryExecuteAsync("PASSP_DeleteUser", new { ID = id }, commandType: CommandType.StoredProcedure);

        #endregion
        // *******************************************************************************************************************************
        #region -  GetClaims  -

        public async Task<IEnumerable<Claim>> GetClaimsAsync(string clientId, string userId, string username)
        {
            var claims = new List<Claim>(7);

            var dto = await GetUserAsync(new Guid(userId)).ConfigureAwait(false);
            if (null != dto)
            {
                // 添加角色
                if (null != dto.Roles)
                {
                    claims.Add(new Claim(CONSTANTs.Claims.Role, dto.Roles.ToString()));
                }

                // 添加权限等级
                if (null != dto.PermissionLevel)
                {
                    claims.Add(new Claim(CONSTANTs.Claims.PermissionLevel, dto.PermissionLevel.ToString()));
                }

                // 添加邮件地址
                if (dto.Email.IsPresent())
                {
                    claims.Add(new Claim(CONSTANTs.Claims.Email, dto.Email));
                }

                // 添加用户账户状态
                claims.Add(new Claim(CONSTANTs.Claims.Status, dto.Status.ToString()));

                // 添加名
                if (dto.FirstName.IsPresent())
                {
                    claims.Add(new Claim(CONSTANTs.Claims.FirstName, dto.FirstName));
                }

                // 添加中间名
                if (dto.MiddleName.IsPresent())
                {
                    claims.Add(new Claim(CONSTANTs.Claims.MiddleName, dto.MiddleName));
                }

                // 添加姓
                if (dto.LastName.IsPresent())
                {
                    claims.Add(new Claim(CONSTANTs.Claims.LastName, dto.LastName));
                }

                // 添加昵称
                var nickName = dto.NickName;  // 使用昵称
                if (nickName.IsMissing())
                {// 如无显示名
                    if (dto.FirstName.IsPresent())
                    {// FirstName存在则使用FirstName
                        nickName = dto.FirstName;
                    }
                    else
                    {// 否则使用用户名
                        nickName = username;
                    }
                }

                claims.Add(new Claim(CONSTANTs.Claims.Nickname, nickName));
            }

            return claims;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  GetUserAsync  -

        public async Task<UserDTO> GetUserAsync(Guid id)
        {
            var mr = await base.TryQueryFirstOrDefaultAsync<UserDTO>("SELECT * FROM SYS_Users WHERE ID = @ID", new { ID = id }).ConfigureAwait(false);
            return mr.Result;
        }

        #endregion
    }
}
