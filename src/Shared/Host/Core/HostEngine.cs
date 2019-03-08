using Microsoft.Extensions.Configuration;
using SyncSoft.App;
using SyncSoft.App.EngineConfigs;
using SyncSoft.App.Redis.Confgiguration;
using SyncSoft.App.Securities;

namespace SyncSoft.Future
{
    public static class HostEngine
    {
        public static CommonConfigurator Init(IConfiguration configuration, string resourceName, bool useRabbitMQ = true)
        {
            var configurator = Engine.Init(configuration)
                .UseECPSeriglogLoggerQuickSettings(options =>
                {
                    options.ConfigSerilogLoggerAppQuickSettingOptions = b =>
                    {
                        b.ConfigAppDefaultComponentsOptions = d =>
                        {
                            d.ConnectionStringProviderType = typeof(RedisConnectionStringProvider);
                        };
                    };
                    options.ConfigECPSecurityComponentsOptions = a =>
                    {
                        a.CoreCertProviderType = typeof(ConfigurationCoreCertProvider);
                    };
                })
                .UseECPAspNetCore(resourceName)
                .UseMessageQueue();

            return useRabbitMQ ? configurator.UseRabbitMQ() : configurator.UseDefaultMessageComponents();
        }
    }
}
