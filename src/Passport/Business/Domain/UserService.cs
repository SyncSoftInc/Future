using SyncSoft.Future.Passport.Command;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain
{
    public class UserService : IUserService
    {
        public Task<string> CreateUserAsync(CreateUserCommand cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
