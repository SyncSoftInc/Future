using SyncSoft.Future.DTO.User;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.DAL.User
{
    public interface IUserDAL
    {
        Task<string> InsertUserAsync(UserDTO dto);
        Task<UserDTO> GetUserAsync(Guid id);
        Task<string> UpdateUserAsync(UserDTO dto);
        Task<string> DeleteUserAsync(Guid id);
    }
}
