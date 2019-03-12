using SyncSoft.Future.Passport.Command;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(CreateUserCommand cmd);
    }
}
