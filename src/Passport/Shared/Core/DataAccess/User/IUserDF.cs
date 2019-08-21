using SyncSoft.Future.DTO.User;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.DataAccess.User
{
    public interface IUserDF
    {
        Task<UserDTO> GetUserAsync(Guid id);
    }
}
