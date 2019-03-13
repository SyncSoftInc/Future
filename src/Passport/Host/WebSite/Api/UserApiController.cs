using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncSoft.App.Components;
using SyncSoft.ECP.AspNetCore.Mvc.Controllers;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.Command.User;
using SyncSoft.Future.Passport.DF.User;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.WebApi
{
    [Area("Api")]
    [BearerAuthorize]
    public abstract class PassportApiController : ApiController
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IUserDF> _lazyUserDF = ObjectContainer.LazyResolve<IUserDF>();
        private IUserDF _UserDF => _lazyUserDF.Value;

        #endregion

        /// <summary>
        /// Create account
        /// </summary>
        [HttpGet("api/user/{id}")]
        public async Task<UserDTO> GetSingleUser(Guid? id)
        {
            if (id.HasValue)
            {
                return await _UserDF.GetUserAsync(id.Value).ConfigureAwait(false);
            }

            return null;
        }

        /// <summary>
        /// Create account
        /// </summary>
        [HttpPost("api/user")]
        public Task<string> CreateUser(CreateUserCommand cmd) => RequestMsgCodeAsync(cmd);
        /// <summary>
        /// Update account
        /// </summary>
        [HttpPut("api/user")]
        public Task<string> UpdateUser(UpdateUserCommand cmd) => RequestMsgCodeAsync(cmd);
        /// <summary>
        /// Create account
        /// </summary>
        [HttpDelete("api/user/{id}")]
        public async Task<string> CreateUser(Guid? id)
        {
            if (id.HasValue)
            {
                return await base.RequestMsgCodeAsync(new DeleteUserCommand { ID = id.Value }).ConfigureAwait(false);
            }

            return MsgCODES.FUT_0000000002;
        }
    }
}
