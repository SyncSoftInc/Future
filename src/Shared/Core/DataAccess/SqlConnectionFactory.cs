//using SyncSoft.App.Components;
//using SyncSoft.Future.Setting;
//using SyncSoft.Future.Setting.Merchant;
//using System;
//using System.Data;
//using System.Threading.Tasks;

//namespace SyncSoft.Future.DataAccess
//{
//    public abstract class SqlConnectionFactory : ISqlConnectionFactory
//    {
//        // *******************************************************************************************************************************
//        #region -  Lazy Object(s)  -

//        private static readonly Lazy<IMerchantSettingProvider> _lazyMerchantSettingProvider = ObjectContainer.LazyResolve<IMerchantSettingProvider>();
//        private IMerchantSettingProvider _MerchantSettingProvider => _lazyMerchantSettingProvider.Value;

//        #endregion
//        // *******************************************************************************************************************************
//        #region -  Property(ies)  -

//        protected abstract string DefaultConnStrName { get; }

//        #endregion
//        // *******************************************************************************************************************************
//        #region -  Create  -

//        public async Task<IDbConnection> CreateAsync(string merchantId)
//        {
//            var connStrName = DefaultConnStrName;
//            var dbSuffix = default(string);

//            if (merchantId.IsPresent())
//            {// 如果指定了Merchant ID

//                // 读取商家配置
//                var merchantSetting = await _MerchantSettingProvider.GetAsync(merchantId).ConfigureAwait(false);
//                if (merchantSetting.IsNotNull())
//                {// 如果有配置存在
//                    OverrideConnStrName(merchantSetting, ref connStrName);
//                    OverrideDbSuffix(merchantSetting, ref dbSuffix);
//                }
//            }

//            var connStr = await _ConnStrProvider.GetAsync(connStrName).ConfigureAwait(false);
//            if (connStr.Contains("{0}"))
//            {
//                connStr = string.Format(connStr, dbSuffix);
//            }

//            return NewConnection(connStr);
//        }


//        #endregion
//        // *******************************************************************************************************************************
//        #region -  NewConnection  -

//        /// <summary>
//        /// 创建新连接
//        /// </summary>
//        protected abstract IDbConnection NewConnection(string connStr);
//        /// <summary>
//        /// 使用商户配置，覆盖连接字符串名
//        /// </summary>
//        protected abstract void OverrideConnStrName(MerchantSetting merchantSetting, ref string connStrName);
//        /// <summary>
//        /// 使用商户配置，覆盖数据库后缀
//        /// </summary>
//        protected abstract void OverrideDbSuffix(MerchantSetting merchantSetting, ref string dbSuffix);

//        #endregion
//    }
//}
