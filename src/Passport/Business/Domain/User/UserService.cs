using SyncSoft.Future.Passport.Command.User;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User
{
    public class UserService : IUserService
    {
        public Task<string> CreateUserAsync(CreateUserCommand cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
