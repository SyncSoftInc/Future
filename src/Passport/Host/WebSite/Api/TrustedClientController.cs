using Microsoft.AspNetCore.Mvc;
using SyncSoft.ECP.Commands.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.DTOs.Users;
using SyncSoft.Future.Enum.User;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Api
{
    public class TrustedClientController : PassportApiController
    {
        [HttpGet("user/{id}")]
        public UserBasicInfoDTO GetUser(Guid? id)
        {
            if (id.HasValue)
            {
                return new UserBasicInfoDTO
                {
                    ID = id.Value,
                    Email = "test@test.com",
                    FirstName = "Jim",
                    LastName = "Green",
                    Roles = (int)ShowRoleEnum.Admin,
                    PermissionLevel = 5
                };
            }
            return null;
        }

        /// <summary>
        /// 验证用户名密码
        /// </summary>
        [HttpPost("account/verify")]
        public Task<AccountDTO> VerifyUsernamePasswordAsync(VerifyUsernamePasswordCommand cmd)
            => RequestAsync<VerifyUsernamePasswordCommand, AccountDTO>(cmd);

        /// <summary>
        /// 创建账户
        /// </summary>
        [HttpPost("account")]
        public Task<string> CreateAccountAsync(CreateAccountCommand cmd)
            => RequestMsgCodeAsync(cmd);
    }
}
