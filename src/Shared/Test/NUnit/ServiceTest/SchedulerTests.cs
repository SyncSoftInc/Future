using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.APIs.Service;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.NUnit.ServiceTest
{
    public abstract class SchedulerTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<ISchedulerApi> _lazySchedulerApi = ObjectContainer.LazyResolve<ISchedulerApi>();
        private ISchedulerApi _SchedulerApi => _lazySchedulerApi.Value;

        #endregion

        [Test]
        public void Get()
        {
            var ar = _SchedulerApi.GetAsync().Execute();
            Assert.IsTrue(ar.IsSuccess);

            var a = ar.GetResultAsync().Execute();
            Assert.IsNotNull(a);
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
