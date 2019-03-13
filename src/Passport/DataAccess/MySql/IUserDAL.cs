using SyncSoft.ECP.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.MySql
{
    public interface IUserDAL
    {
        Task<string> InsertUserAsync(UserBasicInfoDTO dto);
        Task<string> UpdateUserAsync(UserBasicInfoDTO dto);
        Task<string> DeleteUserAsync(Guid id);
    }
}
