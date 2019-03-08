using SyncSoft.App.Collections;
using SyncSoft.App.Components;
using SyncSoft.App.DataAccess;
using SyncSoft.App.MySql;
using SyncSoft.Future.Setting;
using SyncSoft.Future.Setting.Merchant;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.MySql
{

    /// <summary>
    /// 支持商家独享的数据库
    /// </summary>
    public abstract class MerchantExclusiveDB : MySqlDatabase
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IMerchantSettingProvider> _lazyMerchantSettingProvider = ObjectContainer.LazyResolve<IMerchantSettingProvider>();
        private IMerchantSettingProvider _MerchantSettingProvider => _lazyMerchantSettingProvider.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Field(s)  -

        public const string Parameter_MerchantId = "MerchantId";
        public const string Parameter_MerchantSetting = "MerchantSetting";

        private static readonly Lazy<SmartDictionary<string, string>> _lazyCache = new Lazy<SmartDictionary<string, string>>(() =>
            ObjectContainer.Resolve<IDictionaryFactory>().CreateSmartDictionary<string, string>()
        );
        private SmartDictionary<string, string> _Cache => _lazyCache.Value;

        #endregion
        // *******************************************************************************************************************************
        #region -  Constructor(s)  -

        public MerchantExclusiveDB(string connStrName) : base(connStrName)
        { }

        #endregion
        // *******************************************************************************************************************************
        #region -  GetConnectionString  -

        protected override async Task<string> GetConnectionStringAsync(SqlDatabaseOptions options)
        {
            if (options.TryGetParameter(Parameter_MerchantId, out string merchantId) && merchantId.IsPresent())
            {// 如果参数里指定了MerchantID

                return await _Cache.GetOrAddAsync(merchantId, async key =>
                {
                    string connStr = default(string);
                    // 读取商家配置
                    var merchantSetting = await _MerchantSettingProvider.GetAsync(merchantId).ConfigureAwait(false);
                    if (merchantSetting.IsNotNull())
                    {
                        if (merchantSetting.WarehouseDbConnstrName.IsPresent())
                        {// 如果配置了连接字符串名
                            connStr = await _ConnectionStringProvider.GetAsync(merchantSetting.WarehouseDbConnstrName).ConfigureAwait(false);
                        }
                    }

                    if (connStr.IsMissing())
                    {
                        connStr = await base.GetConnectionStringAsync(options).ConfigureAwait(false);
                    }

                    if (connStr.IsPresent())
                    {
                        connStr = FormatConnectionString(connStr, merchantSetting);
                    }

                    return connStr;
                }).ConfigureAwait(false);
            }

            // 否则使用默认连接字符串名
            return await base.GetConnectionStringAsync(options).ConfigureAwait(false);
        }

        #endregion
        // *******************************************************************************************************************************
        #region -  FormatConnectionString  -

        private string FormatConnectionString(string connStr, MerchantSetting merchantSetting)
        {
            if (connStr.Contains("{0}")                             // 如果连接字符串里允许传入参数
                && merchantSetting.IsNotNull()                      // 并且商家配置不为空
                && merchantSetting.WarehouseDbSuffix.IsPresent()    // 并且配置了数据库后缀
            )
            {
                // 将数据库后缀添加到连接字符串
                return string.Format(connStr, merchantSetting.WarehouseDbSuffix);
            }

            return connStr;
        }

        #endregion
    }
}
