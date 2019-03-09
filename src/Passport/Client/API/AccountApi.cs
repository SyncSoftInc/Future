using SyncSoft.App;
using SyncSoft.App.Http;
using SyncSoft.App.WebApi.Auth;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.WebApi.ApiProxies;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.API
{
    public class AccountApi : ECPApiProxyBase, IAccountApi
    {
        protected override string UriKey => "passport";

        public Task<HttpResult<string>> CreateAccountAsync(object cmd)
            => base.PostAsync<string>(BearerAuthModeEnum.Client, "account", cmd);

        public Task<HttpResult<string>> ChangePasswordAsync(object cmd)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<string>> ChangePasswordDirectlyAsync(object cmd)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<string>> ChangeStatusAsync(object cmd)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<string>> ChangeUsernameDirectlyAsync(object cmd)
        {
            throw new NotImplementedException();
        }


        public Task<HttpResult<string>> CreateLoginTokenAsync(object cmd)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<string>> CreateResetPasswordTokenAsync(object cmd)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<string>> DeleteAccountAsync(object cmd)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<AccountDTO>> GetAccountAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<AccountDTO>> GetAccountAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<MsgResult<AccountDTO>>> LoginAsync(object cmd)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<string>> ResetLoginFailedCountAsync(object cmd)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<string>> ResetPasswordAsync(object cmd)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult<AccountDTO>> VerifyUsernamePasswordAsync(object cmd)
            => base.PostAsync<AccountDTO>(BearerAuthModeEnum.Client, "account/verification", cmd);
    }
}
