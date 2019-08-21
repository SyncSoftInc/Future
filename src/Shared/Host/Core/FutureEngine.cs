using SyncSoft.App;
using SyncSoft.App.EngineConfigs;
using SyncSoft.App.Redis.Messaging;
using SyncSoft.App.Redis.Transaction;
using SyncSoft.App.Securities;
using SyncSoft.ECP.Securities;

namespace SyncSoft.Future
{
    public static class FutureEngine
    {
        public static CommonConfigurator Init(string projectName
            , string[] args
            , bool allowOverridingRegistrations = false
        )
        {
            return Engine.Init(options =>
            {
                options.AllowOverridingRegistrations = allowOverridingRegistrations;
                options.ConsoleArguments = args;
            })
                .UseEcpHostQuickSettings(options =>
                {
                    options.ProjectName = projectName;
                    options.ConfigAppDefaultComponentsOptions = d =>
                    {
                        d.MsgResultStoreType = typeof(RedisMsgResultStore);
                        d.TransactionStateStoreType = typeof(RedisTransactionStateStore);
                    };
                    options.ConfigECPSecurityComponentsOptions = a =>
                    {
                        a.CoreCertProviderType = typeof(ConfigurationCoreCertProvider);
                        a.PasswordEncryptorType = typeof(Sha256PasswordEncryptor);
                    };
                });
        }
    }
}
