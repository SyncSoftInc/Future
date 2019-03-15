using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UsePassportAPI(this CommonConfigurator configurator)
        {
            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<SyncSoft.ECP.APIs.Account.IAccountApi, SyncSoft.Future.Passport.API.Account.AccountApi>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<SyncSoft.Future.Passport.API.Account.IAccountApi, SyncSoft.Future.Passport.API.Account.AccountApi>(LifeCycleEnum.Singleton);

                    ObjectContainer.Register<SyncSoft.ECP.APIs.User.IUserApi, SyncSoft.Future.Passport.API.User.UserApi>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<SyncSoft.Future.Passport.API.User.IUserApi, SyncSoft.Future.Passport.API.User.UserApi>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
