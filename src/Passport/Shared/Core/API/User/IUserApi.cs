using SyncSoft.App.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.API.User
{
    public interface IUserApi : SyncSoft.ECP.APIs.User.IUserApi
    {
        Task<HttpResult<string>> CreateUserAsync(object cmd);
        Task<HttpResult<string>> UserSaveProfileAsync(object cmd);
        Task<HttpResult<string>> DeleteUserAsync(object cmd);
    }
}
