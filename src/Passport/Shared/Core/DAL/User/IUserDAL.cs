using System;
using System.Threading.Tasks;
using SyncSoft.Future.DTO.User;

namespace SyncSoft.Future.Passport.DAL.User
{
    public interface IUserDAL
    {
        Task<UserDTO> GetUserAsync(Guid id);
    }
}
