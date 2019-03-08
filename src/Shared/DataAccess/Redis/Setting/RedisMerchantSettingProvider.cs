using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.ECP.Redis.Setting;
using SyncSoft.Future.Setting;
using SyncSoft.Future.Setting.Merchant;
using System;

namespace SyncSoft.Future.Redis.Setting
{
    public class RedisMerchantSettingProvider : RedisGenericObjectProvider<MerchantSetting>, IMerchantSettingProvider
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<RedisMerchantSettingProvider>();
        protected override ILogger _Logger => _lazyLogger.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Constructor(s)  -

        public RedisMerchantSettingProvider(IMerchantSettingDB db) : base(db)
        {
        }

        #endregion
    }
}
