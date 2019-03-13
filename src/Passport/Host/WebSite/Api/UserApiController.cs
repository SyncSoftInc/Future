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
        /// Get single user
        /// </summary>
        [HttpGet("api/user/{id}")]
        public async Task<UserDTO> GetSingleUserAsync(Guid? id)
        {
            if (id.HasValue)
            {
                return await _UserDF.GetUserAsync(id.Value).ConfigureAwait(false);
            }

            return null;
        }

        /// <summary>
        /// Create user
        /// </summary>
        [HttpPost("api/user")]
        public Task<string> CreateUserAsync(CreateUserCommand cmd) => RequestMsgCodeAsync(cmd);

        /// <summary>
        /// Update user
        /// </summary>
        [HttpPut("api/user")]
        public Task<string> UpdateUserAsync(UpdateUserCommand cmd) => RequestMsgCodeAsync(cmd);

        /// <summary>
        /// User save profile
        /// </summary>
        [HttpPut("api/user/profile")]
        public Task<string> UserSaveProfileAsync(UserSaveProfileCommand cmd) => RequestMsgCodeAsync(cmd);

        /// <summary>
        /// Delete user
        /// </summary>
        [HttpDelete("api/user/{id}")]
        public async Task<string> DeleteUserAsync(Guid? id)
        {
            if (id.HasValue)
            {
                return await base.RequestMsgCodeAsync(new DeleteUserCommand { ID = id.Value }).ConfigureAwait(false);
            }

            return MsgCODES.FUT_0000000002;
        }
    }
}
