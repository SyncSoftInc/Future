using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncSoft.App.Components;
using SyncSoft.ECP.AspNetCore.Mvc.Controllers;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.Command.User;
using SyncSoft.Future.Passport.DataAccess.User;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Api
{
    [Area("Api")]
    [Route("api")]
    [BearerAuthorize]
    public class UserApiController : ApiController
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IUserDF> _lazyUserDF = ObjectContainer.LazyResolve<IUserDF>();
        private IUserDF _UserDF => _lazyUserDF.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  GetSingleUser  -

        /// <summary>
        /// Get single user
        /// </summary>
        [HttpGet("user/{id}")]
        public async Task<UserDTO> GetSingleUserAsync(Guid? id)
        {
            if (id.HasValue)
            {
                return await _UserDF.GetUserAsync(id.Value).ConfigureAwait(false);
            }

            return null;
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  CreateUser  -

        /// <summary>
        /// Create user
        /// </summary>
        [HttpPost("user")]
        public Task<string> CreateUserAsync(CreateUserCommand cmd) => RequestAsync(cmd);

        #endregion
        // *******************************************************************************************************************************
        #region -  UpdateUser  -

        /// <summary>
        /// Update user
        /// </summary>
        [HttpPut("user")]
        public Task<string> UpdateUserAsync(UpdateUserCommand cmd) => RequestAsync(cmd);

        #endregion
        // *******************************************************************************************************************************
        #region -  UserSaveProfile  -

        /// <summary>
        /// User save profile
        /// </summary>
        [HttpPut("user/profile")]
        public Task<string> UserSaveProfileAsync(UserSaveProfileCommand cmd) => RequestAsync(cmd);

        #endregion
        // *******************************************************************************************************************************
        #region -  DeleteUser  -

        /// <summary>
        /// Delete user
        /// </summary>
        [HttpDelete("user/{id}")]
        public async Task<string> DeleteUserAsync(Guid? id)
        {
            if (id.HasValue)
            {
                return await base.RequestAsync(new DeleteUserCommand { ID = id.Value }).ConfigureAwait(false);
            }

            return MsgCODES.FUT_0000000002;
        }

        #endregion
    }
}
