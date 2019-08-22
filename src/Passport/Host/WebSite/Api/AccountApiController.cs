using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncSoft.App.Components;
using SyncSoft.ECP.AspNetCore.Mvc.Controllers;
using SyncSoft.ECP.Commands.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.Future.Passport.DataAccess.User;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Api
{
    [Area("Api")]
    [Route("api")]
    [BearerAuthorize]
    public class AccountApiController : ApiController
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IUserDF> _lazyUserDF = ObjectContainer.LazyResolve<IUserDF>();
        private IUserDF _UserDF => _lazyUserDF.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  CreateAccount  -

        /// <summary>
        /// Create user
        /// </summary>
        [HttpPost("account")]
        public Task<string> CreateAccountAsync(CreateAccountCommand cmd) => RequestAsync(cmd);

        #endregion
        // *******************************************************************************************************************************
        #region -  DeleteAccount  -

        /// <summary>
        /// Create user
        /// </summary>
        [HttpDelete("account")]
        public Task<string> DeleteAccountAsync(DeleteAccountCommand cmd) => RequestAsync(cmd);

        #endregion
        // *******************************************************************************************************************************
        #region -  CreateAccount  -

        /// <summary>
        /// Create user
        /// </summary>
        [HttpPost("account/verification")]
        public Task<AccountDTO> VerifyAccountAsync(VerifyUsernamePasswordCommand cmd) => RequestAsync<VerifyUsernamePasswordCommand, AccountDTO>(cmd);

        #endregion
    }
}
