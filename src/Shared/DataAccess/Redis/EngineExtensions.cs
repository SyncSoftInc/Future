using SyncSoft.App.Components;
using SyncSoft.App.EngineConfigs;
using SyncSoft.Future.Redis;
using SyncSoft.Future.Redis.Setting;
using SyncSoft.Future.Setting;

namespace SyncSoft.App
{
    public static class EngineExtensions
    {
        public static CommonConfigurator UseFutureRedis(this CommonConfigurator configurator)
        {
            Engine.PreventDuplicateRegistration(nameof(UseFutureRedis));

            configurator.Engine.Starting += (o, e) =>
            {
                ObjectContainer.Register<IMerchantSettingProvider, RedisMerchantSettingProvider>(LifeCycleEnum.Singleton);
                ObjectContainer.Register<IMerchantSettingDB>(() => new MerchantSettingDB(CONSTANTS.CONNECTION_STRINGS.REDIS_DEFAULT), LifeCycleEnum.Singleton);

                    //ObjectContainer.Register<IShowConnStrProvider, RedisShowConnStrProvider>(LifeCycleEnum.Singleton);
                    //ObjectContainer.Register<IConnStrDB>(() => new ConnStrDB(CONSTANTS.CONNECTION_STRINGS.REDIS_DEFAULT), LifeCycleEnum.Singleton);
                };

            return configurator;
        }
    }
}
