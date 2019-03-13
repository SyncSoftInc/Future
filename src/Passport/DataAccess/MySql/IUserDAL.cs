using SyncSoft.ECP.DTOs.Users;
using SyncSoft.Future.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.MySql
{
    public interface IUserDAL
    {
        Task<string> InsertUserAsync(UserDTO dto);
        Task<string> UpdateUserAsync(UserDTO dto);
        Task<string> DeleteUserAsync(Guid id);
    }
}
