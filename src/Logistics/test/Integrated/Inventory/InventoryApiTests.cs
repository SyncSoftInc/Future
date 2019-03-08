using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.Future.Logistics.API.Inventory;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.IntegratedTest.Inventory
{
    public class InventoryApiTests
    {
        private static readonly Lazy<IInventoryApi> _lazyInventoryApi = ObjectContainer.LazyResolve<IInventoryApi>();
        private IInventoryApi _InventoryApi => _lazyInventoryApi.Value;

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger(nameof(InventoryApiTests));
        private ILogger _Logger => _lazyLogger.Value;

        [Test]
        public void GetAvailableInventory()
        {
            var merchantId = "46e02472512f4ed1b388665fa2a1ea7c";

            var a = _InventoryApi.GetAvailableInventoryAsync(merchantId, "ITEM1").Execute();

            _Logger.Debug("{0}", a);

            Assert.IsTrue(a.IsSuccess, a.GetMsgCodeAsync().Execute());
        }
    }
}
