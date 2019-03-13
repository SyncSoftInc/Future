using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.Future.Passport.MySql;
using System;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UsePassportMySql(this CommonConfigurator configurator
            , Action<PassportMySqlOptions> configOptions = null
            , PassportMySqlOptions options = null)
        {
            options = options ?? new PassportMySqlOptions();
            configOptions?.Invoke(options);

            if (!Engine.IsStarted)
            {
                configurator.Engine.Starting += (o, e) =>
                {
                    ObjectContainer.Register<IPassportDB>(() => new PassportDB(options.ConnStrName), LifeCycleEnum.Singleton);

                    ObjectContainer.Register<IAccountDAL, AccountDAL>(LifeCycleEnum.Singleton);
                    ObjectContainer.Register<IUserDAL, UserDAL>(LifeCycleEnum.Singleton);
                };
            }

            return configurator;
        }
    }
}
