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
        [HttpPost("account/verification")]
        public Task<AccountDTO> VerifyUsernamePassword(VerifyUsernamePasswordCommand cmd)
            => RequestAsync<VerifyUsernamePasswordCommand, AccountDTO>(cmd);

        /// <summary>
        /// Account login
        /// </summary>
        [HttpPost("account/login")]
        public Task<string> Login(LoginCommand cmd) => RequestMsgCodeAsync(cmd);

        /// <summary>
        /// Reset Account password
        /// </summary>
        [HttpPut("account/password")]
        public Task<string> ResetPassword(ResetPasswordCommand cmd) => RequestMsgCodeAsync(cmd);

        /// <summary>
        /// Create Account login token
        /// </summary>
        [HttpPost("account/loginToken")]
        public Task<string> CreateLoginToken(CreateLoginTokenCommand cmd) => RequestMsgCodeAsync(cmd);

        /// <summary>
        /// Create Account reset password token
        /// </summary>
        [HttpPost("account/passwordToken")]
        public Task<string> CreateResetPasswordToken(CreateResetPasswordTokenCommand cmd) => RequestMsgCodeAsync(cmd);
    }
}
