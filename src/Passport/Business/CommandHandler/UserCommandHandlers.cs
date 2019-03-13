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
    }
}
