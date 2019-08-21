using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.ECP.Identity;
using SyncSoft.Future.Passport.DAL.User;
using SyncSoft.Future.Passport.MySql;
using SyncSoft.Future.Passport.MySql.Account;
using System;

namespace SyncSoft.App
{
    public static class EngineExtension
    {
        public static CommonConfigurator UsePassportMySql(this CommonConfigurator configurator
            , Action<PassportMySqlOptions> configOptions = null
            , PassportMySqlOptions options = null)
        {
            Engine.PreventDuplicateRegistration(nameof(UsePassportMySql));

            options = options ?? new PassportMySqlOptions();
            configOptions?.Invoke(options);

            configurator.Engine.Starting += (o, e) =>
            {
                ObjectContainer.Register<IPassportDB>(() => new PassportDB(options.ConnStrName), LifeCycleEnum.Singleton);

                ObjectContainer.Register<IAccountDAL, AccountDAL>(LifeCycleEnum.Singleton);
                ObjectContainer.Register<IUserProfileProvider, UserDAL>(LifeCycleEnum.Singleton);
                ObjectContainer.Register<IUserDAL, UserDAL>(LifeCycleEnum.Singleton);
            };

            return configurator;
        }
    }
}
