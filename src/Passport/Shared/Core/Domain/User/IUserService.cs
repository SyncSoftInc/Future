using SyncSoft.Future.Passport.Command.User;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(CreateUserCommand cmd);
        Task<string> UpdateUserAsync(UpdateUserCommand cmd);
        Task<string> UserSaveProfileAsync(UserSaveProfileCommand cmd);
        Task<string> DeleteUserAsync(DeleteUserCommand cmd);
    }
}
