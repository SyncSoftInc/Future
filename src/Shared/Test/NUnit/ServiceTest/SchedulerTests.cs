using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.APIs.Service;
using System;
using System.Threading.Tasks;
using SyncSoft.App.Logging;

namespace SyncSoft.Future.NUnit.ServiceTest
{
    public abstract class SchedulerTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<ISchedulerApi> _lazySchedulerApi = ObjectContainer.LazyResolve<ISchedulerApi>();
        private ISchedulerApi _SchedulerApi => _lazySchedulerApi.Value;

        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<SchedulerTests>();
        private ILogger _Logger => _lazyLogger.Value;

        #endregion

        [Test]
        public void Get()
        {
            var ar = _SchedulerApi.GetAsync().Execute();
            var rs = ar.GetResultAsync().Execute();

            _Logger.Debug($"{rs.SchedulerName}");

            Assert.IsNotNull(rs);
        }

        [Test]
        public void Start()
        {
            var msgCode = _SchedulerApi.StartAsync().ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test]
        public void StandBy()
        {
            var msgCode = _SchedulerApi.StandbyAsync().ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }
    }
}
