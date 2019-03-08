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
                    ObjectContainer.Register<SyncSoft.ECP.APIs.Account.IAccountApi, SyncSoft.Future.Passport.API.AccountApi>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<SyncSoft.Future.Passport.API.IAccountApi, SyncSoft.Future.Passport.API.AccountApi>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
