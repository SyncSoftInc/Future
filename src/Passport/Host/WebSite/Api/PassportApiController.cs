using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncSoft.ECP.AspNetCore.Mvc.Controllers;

namespace SyncSoft.Future.Passport.Api
{
    [Area("Api")]
    [BearerAuthorize]
    public abstract class PassportApiController : ApiController
    {

    }
}
