using SyncSoft.App.Components;
using SyncSoft.App.Messaging;
using SyncSoft.ECP.Commands.Account;
using SyncSoft.Future.Passport.Domain;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.CommandHandler
{
    public class AccountCommandHandlers
            : IConsumer<LoginCommand>
            , IConsumer<VerifyUsernamePasswordCommand>
            , IConsumer<CreateAccountCommand>
            , IConsumer<DeleteAccountCommand>
            , IConsumer<ChangePasswordCommand>
            , IConsumer<ChangePasswordDirectlyCommand>
            , IConsumer<ChangeUsernameDirectlyCommand>
            , IConsumer<ChangeStatusCommand>
            , IConsumer<ResetLoginFailedCountCommand>
            , IConsumer<CreateLoginTokenCommand>
            , IConsumer<CreateResetPasswordTokenCommand>
            , IConsumer<ResetPasswordCommand>
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IAccountService> _lazyAccountService = ObjectContainer.LazyResolve<IAccountService>();
        private IAccountService _AccountService => _lazyAccountService.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  CreateAccountCommand  -

        public async Task<object> HandleAsync(IContext<CreateAccountCommand> context) => await _AccountService.CreateAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  DeleteAccountCommand  -

        public async Task<object> HandleAsync(IContext<DeleteAccountCommand> context) => await _AccountService.DeleteAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  ChangePasswordCommand  -

        public async Task<object> HandleAsync(IContext<ChangePasswordCommand> context) => await _AccountService.ChangePasswordAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  ChangePasswordDirectlyCommand  -

        public async Task<object> HandleAsync(IContext<ChangePasswordDirectlyCommand> context) => await _AccountService.ChangePasswordAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  ChangeUsernameDirectlyCommand  -

        public async Task<object> HandleAsync(IContext<ChangeUsernameDirectlyCommand> context) => await _AccountService.ChangeUsernameAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  ChangeStatusCommand  -
        public async Task<object> HandleAsync(IContext<ChangeStatusCommand> context) => await _AccountService.ChangeStatusAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  ResetLoginFailedCountCommand  -
        public async Task<object> HandleAsync(IContext<ResetLoginFailedCountCommand> context) => await _AccountService.ResetLoginFailedCountAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  LoginCommand  -

        public async Task<object> HandleAsync(IContext<LoginCommand> context) => await _AccountService.LoginAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  VerifyUsernamePasswordCommand  -

        public async Task<object> HandleAsync(IContext<VerifyUsernamePasswordCommand> context)
            => await _AccountService.VerifyUsernamePasswordAsync(context.Message.Username, context.Message.Password).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  CreateLoginTokenCommand  -

        public async Task<object> HandleAsync(IContext<CreateLoginTokenCommand> context)
            => await _AccountService.CreateLoginTokenAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  CreateResetPasswordTokenCommand  -

        public async Task<object> HandleAsync(IContext<CreateResetPasswordTokenCommand> context)
            => await _AccountService.CreateResetPasswordTokenAsync(context.Message).ConfigureAwait(false);

        #endregion
        // *******************************************************************************************************************************
        #region -  ResetPasswordCommand  -

        public async Task<object> HandleAsync(IContext<ResetPasswordCommand> context)
            => await _AccountService.ResetPasswordAsync(context.Message).ConfigureAwait(false);

        #endregion
    }
}
