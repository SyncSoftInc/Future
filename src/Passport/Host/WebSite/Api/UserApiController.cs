using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncSoft.ECP.AspNetCore.Mvc.Controllers;
using SyncSoft.Future.Passport.Command;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.WebApi
{
    [Area("Api")]
    [BearerAuthorize]
    public abstract class PassportApiController : ApiController
    {
        /// <summary>
        /// Create account
        /// </summary>
        [HttpPost("api/user")]
        public Task<string> CreateUser(CreateUserCommand cmd) => RequestMsgCodeAsync(cmd);
    }
}
