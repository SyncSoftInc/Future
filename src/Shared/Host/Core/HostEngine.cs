using Microsoft.Extensions.Configuration;
using SyncSoft.App;
using SyncSoft.App.EngineConfigs;
using SyncSoft.App.Redis.Confgiguration;
using SyncSoft.App.Securities;
using SyncSoft.ECP.Securities;

namespace SyncSoft.Future
{
    public static class HostEngine
    {
        public static CommonConfigurator Init(IConfiguration configuration, string resourceName, bool useRabbitMQ = true, bool allowOverridingRegistrations = false)
        {
            var configurator = Engine.Init(options =>
            {
                options.Configuration = configuration;
                options.AllowOverridingRegistrations = allowOverridingRegistrations;
            })
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
                        a.PasswordEncryptorType = typeof(Sha256PasswordEncryptor);
                    };
                })
                .UseECPAspNetCore(resourceName)
                .UseMessageQueue();

            return useRabbitMQ ? configurator.UseRabbitMQ() : configurator.UseDefaultMessageComponents();
        }
    }
}
