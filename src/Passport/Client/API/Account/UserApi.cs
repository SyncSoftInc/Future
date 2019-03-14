using SyncSoft.App.Http;
using SyncSoft.App.WebApi.Auth;
using SyncSoft.ECP.APIs.User;
using SyncSoft.ECP.DTOs.Users;
using SyncSoft.ECP.WebApi.ApiProxies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.API.Account
{
    public class UserApi : ECPApiProxyBase, IUserApi
    {
        protected override string UriKey => "passport";

        public Task<HttpResult<UserBasicInfoDTO>> GetUserBasicInfoAsync(Guid id)
            => base.PostAsync<UserBasicInfoDTO>(BearerAuthModeEnum.Client, $"api/user/{id}");

        public Task<HttpResult<string>> UpdateUserAsync(object cmd)
        {
            throw new NotImplementedException();
        }
    }
}
