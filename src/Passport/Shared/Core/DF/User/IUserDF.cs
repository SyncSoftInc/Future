using SyncSoft.Future.DTO.User;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.DF.User
{
    public interface IUserDF
    {
        Task<UserDTO> GetUserAsync(Guid id);
    }
}
