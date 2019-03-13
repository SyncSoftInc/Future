using SyncSoft.App;
using SyncSoft.App.Http;
using SyncSoft.App.WebApi.Auth;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.WebApi.ApiProxies;
using SyncSoft.Future.Passport.API.Account;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.API.Account
{
    public class AccountApi : ECPApiProxyBase, IAccountApi
    {
        protected override string UriKey => "passport";

        public Task<HttpResult<string>> CreateAccountAsync(object cmd)
            => base.PostAsync<string>(BearerAuthModeEnum.Client, "account", cmd);

        public Task<HttpResult<string>> DeleteAccountAsync(object cmd)
            => base.DeleteAsync<string>(BearerAuthModeEnum.Client, "account", cmd);

        public Task<HttpResult<string>> ChangePasswordAsync(object cmd)
            => base.PatchAsync<string>(BearerAuthModeEnum.Client, "account/password", cmd);

        public Task<HttpResult<string>> ChangePasswordDirectlyAsync(object cmd)
            => base.PostAsync<string>(BearerAuthModeEnum.Client, "account/password", cmd);

        public Task<HttpResult<string>> ChangeStatusAsync(object cmd)
            => base.PatchAsync<string>(BearerAuthModeEnum.Client, "account/status", cmd);

        public Task<HttpResult<string>> ChangeUsernameDirectlyAsync(object cmd)
            => base.PatchAsync<string>(BearerAuthModeEnum.Client, "account/username", cmd);

        public Task<HttpResult<string>> CreateLoginTokenAsync(object cmd)
            => base.PostAsync<string>(BearerAuthModeEnum.Client, "account/loginToken", cmd);

        public Task<HttpResult<string>> CreateResetPasswordTokenAsync(object cmd)
            => base.PostAsync<string>(BearerAuthModeEnum.Client, "account/passwordToken", cmd);

        public Task<HttpResult<MsgResult<AccountDTO>>> LoginAsync(object cmd)
            => base.PostAsync<MsgResult<AccountDTO>>(BearerAuthModeEnum.Client, "account/login", cmd);

        public Task<HttpResult<string>> ResetLoginFailedCountAsync(object cmd)
            => base.PatchAsync<string>(BearerAuthModeEnum.Client, "account/failedCount", cmd);

        public Task<HttpResult<string>> ResetPasswordAsync(object cmd)
            => base.PutAsync<string>(BearerAuthModeEnum.Client, "account/password", cmd);

        public Task<HttpResult<AccountDTO>> VerifyUsernamePasswordAsync(object cmd)
            => base.PostAsync<AccountDTO>(BearerAuthModeEnum.Client, "account/verification", cmd);

        public Task<HttpResult<AccountDTO>> GetAccountAsync(Guid id)
            => base.GetAsync<AccountDTO>(BearerAuthModeEnum.Client, "account", id);

        public Task<HttpResult<AccountDTO>> GetAccountAsync(string username)
            => base.GetAsync<AccountDTO>(BearerAuthModeEnum.Client, "account", username);
    }
}
