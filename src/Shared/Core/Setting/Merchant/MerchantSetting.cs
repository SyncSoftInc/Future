namespace SyncSoft.Future.Setting.Merchant
{
    public class MerchantSetting
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string Merchant_ID { get; set; }

        /// <summary>
        /// 仓库连接字符串名
        /// </summary>
        public string WarehouseDbConnstrName { get; set; }

        /// <summary>
        /// 仓库数据库后缀
        /// </summary>
        public string WarehouseDbSuffix { get; set; }

        /// <summary>
        /// 最多允许的仓库个数
        /// </summary>
        public int MaxWarehouseLimit { get; set; } = 1;

        /// <summary>
        /// 最多允许的商品项个数
        /// </summary>
        public int MaxUpcLimit { get; set; } = 100;
    }
}
