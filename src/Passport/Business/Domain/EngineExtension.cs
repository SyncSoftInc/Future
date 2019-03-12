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
                    ObjectContainer.Register<SyncSoft.ECP.Domains.Account.IAccountService, SyncSoft.Future.Passport.Domain.AccountService>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<SyncSoft.Future.Passport.Domain.IAccountService, SyncSoft.Future.Passport.Domain.AccountService>(LifeCycleEnum.Singleton);

                    ObjectContainer.Register<SyncSoft.Future.Passport.Domain.IUserService, SyncSoft.Future.Passport.Domain.UserService>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
