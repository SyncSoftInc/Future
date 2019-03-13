using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UsePassportDomain(this CommonConfigurator configurator)
        {
            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<SyncSoft.ECP.Domains.Account.IAccountService, SyncSoft.Future.Passport.Domain.Account.AccountService>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<SyncSoft.Future.Passport.Domain.Account.IAccountService, SyncSoft.Future.Passport.Domain.Account.AccountService>(LifeCycleEnum.Singleton);

                    ObjectContainer.Register<SyncSoft.Future.Passport.Domain.User.IUserService, SyncSoft.Future.Passport.Domain.User.UserService>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
