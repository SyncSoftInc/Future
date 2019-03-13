using SyncSoft.App.Components;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.DAL.User;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.DF.User
{
    public class UserDF : IUserDF
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IUserDAL> _lazyUserDAL = ObjectContainer.LazyResolve<IUserDAL>();
        private IUserDAL _UserDAL => _lazyUserDAL.Value;

        #endregion
        public Task<UserDTO> GetUserAsync(Guid id)
        {
            return _UserDAL.GetUserAsync(id);
        }
    }
}
