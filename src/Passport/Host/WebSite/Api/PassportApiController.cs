//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using SyncSoft.ECP.AspNetCore.Mvc.Controllers;
//using SyncSoft.ECP.Commands.Account;
//using SyncSoft.ECP.DTOs.Account;
//using System;
//using System.Threading.Tasks;

//namespace SyncSoft.Future.Passport.WebApi
//{
//    [Area("Api")]
//    [BearerAuthorize]
//    public abstract class PassportApiController : ApiController
//    {
//        /// <summary>
//        /// Create account
//        /// </summary>
//        [HttpPost("account")]
//        public Task<string> CreateAccount(CreateAccountCommand cmd) => RequestMsgCodeAsync(cmd);

//        /// <summary>
//        /// Delete Account
//        /// </summary>
//        [HttpDelete("account/{id}")]
//        public Task<string> DeleteAccount(Guid? id)
//        {
//            if (id.HasValue)
//            {
//                return RequestMsgCodeAsync(new DeleteAccountCommand { ID = id.Value });
//            }
//            else
//            {
//                return Task.FromResult(MsgCODES.FUT_0000000002);
//            }
//        }

//        /// <summary>
//        /// Change Account Username
//        /// </summary>
//        [HttpPatch("account/username")]
//        public Task<string> ChangeUsername(ChangeUsernameDirectlyCommand cmd) => RequestMsgCodeAsync(cmd);

//        /// <summary>
//        /// Change Account Status
//        /// </summary>
//        [HttpPatch("account/status")]
//        public Task<string> ChangeStatus(ChangeStatusCommand cmd) => RequestMsgCodeAsync(cmd);

//        /// <summary>
//        /// Change Account Password
//        /// </summary>
//        [HttpPatch("account/password")]
//        public Task<string> ChangePassword(ChangePasswordCommand cmd) => RequestMsgCodeAsync(cmd);

//        /// <summary>
//        /// Change Account Password
//        /// </summary>
//        [HttpPost("account/password")]
//        public Task<string> ChangePasswordDirectly(ChangePasswordDirectlyCommand cmd) => RequestMsgCodeAsync(cmd);

//        /// <summary>
//        /// Reset Account login failed count
//        /// </summary>
//        [HttpPatch("account/failedCount")]
//        public Task<string> ResetLoginFailedCount(ResetLoginFailedCountCommand cmd) => RequestMsgCodeAsync(cmd);

//        /// <summary>
//        /// Get Account by id
//        /// </summary>
//        [HttpGet("account/{id}")]
//        public Task<AccountDTO> GetAccount(Guid id)
//        {
//            return Task.FromResult(new AccountDTO
//            {
//                ID = id,
//                Username = "Username",
//                Password = "Password",
//                PasswordSalt = "PasswordSalt",
//                LoginFailedCount = 0,
//                LastLoginIP = "LastLoginIP",
//                Status = ECP.Enums.Account.AccountStatusEnum.Active,
//                UpdatedOnUtc = DateTime.UtcNow,
//                CreatedOnUtc = DateTime.UtcNow,
//                LastLoginUtc = DateTime.UtcNow,
//            });
//        }

//        /// <summary>
//        /// Get Account by username
//        /// </summary>
//        [HttpGet("account/{username}")]
//        public Task<AccountDTO> GetAccount(string username)
//        {
//            return Task.FromResult(new AccountDTO
//            {
//                ID = new Guid("ec35e329dac34842af18f0028763a059"),
//                Username = username,
//                Password = "Password",
//                PasswordSalt = "PasswordSalt",
//                LoginFailedCount = 0,
//                LastLoginIP = "LastLoginIP",
//                Status = ECP.Enums.Account.AccountStatusEnum.Active,
//                UpdatedOnUtc = DateTime.UtcNow,
//                CreatedOnUtc = DateTime.UtcNow,
//                LastLoginUtc = DateTime.UtcNow,
//            });
//        }
//    }
//}
