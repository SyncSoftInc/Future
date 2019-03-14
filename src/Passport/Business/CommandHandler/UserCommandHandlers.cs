using SyncSoft.App.Components;
using SyncSoft.App.Messaging;
using SyncSoft.Future.Passport.Command.User;
using SyncSoft.Future.Passport.Domain.User;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.CommandHandler
{
    public class UserCommandHandlers
        : IConsumer<CreateUserCommand>
        , IConsumer<UpdateUserCommand>
        , IConsumer<UserSaveProfileCommand>
        , IConsumer<DeleteUserCommand>
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IUserService> _lazyUserService = ObjectContainer.LazyResolve<IUserService>();
        private IUserService _UserService => _lazyUserService.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  CreateUserCommand  -

        public async Task<object> HandleAsync(IContext<CreateUserCommand> context)
        {
            return await _UserService.CreateUserAsync(context.Message).ConfigureAwait(false);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UpdateUserCommand  -

        public async Task<object> HandleAsync(IContext<UpdateUserCommand> context)
        {
            return await _UserService.UpdateUserAsync(context.Message).ConfigureAwait(false);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  UserSaveProfileCommand  -

        public async Task<object> HandleAsync(IContext<UserSaveProfileCommand> context)
        {
            return await _UserService.UserSaveProfileAsync(context.Message).ConfigureAwait(false);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  DeleteUserCommand  -

        public async Task<object> HandleAsync(IContext<DeleteUserCommand> context)
        {
            return await _UserService.DeleteUserAsync(context.Message).ConfigureAwait(false);
        }

        #endregion
    }
}
