using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.APIs.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncSoft.Future.NUnit.ServiceTest
{
    public abstract class TriggerTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<ITriggerApi> _lazyTriggerApi = ObjectContainer.LazyResolve<ITriggerApi>();
        private ITriggerApi _TriggerApi => _lazyTriggerApi.Value;

        #endregion

        protected abstract ServiceTestCase CreateTestCase();

        [Test, Order(0)]
        public void Create()
        {
            var testCase = CreateTestCase();
            var cmd = new
            {
                testCase.JobName,
                testCase.JobGroupName,
                testCase.TriggerGroupName,
                testCase.TriggerName,
                testCase.IntervalSeconds
            };

            var msgCode = _TriggerApi.CreateAsync(cmd).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test, Order(25)]
        public void Resume()
        {
            var testCase = CreateTestCase();
            var msgCode = _TriggerApi.ResumeAsync(new
            {
                Name = testCase.TriggerName,
                GroupName = testCase.TriggerGroupName
            }).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test, Order(50)]
        public void Pause()
        {
            var testCase = CreateTestCase();
            var msgCode = _TriggerApi.PauseAsync(new
            {
                Name = testCase.TriggerName,
                GroupName = testCase.TriggerGroupName
            }).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test, Order(75)]
        public void Update()
        {
            var testCase = CreateTestCase();
            var cmd = new
            {
                Name = testCase.TriggerName,
                NewName = testCase.TriggerName + "_UPDATE",
                GroupName = testCase.TriggerGroupName,
                NewGroupName = testCase.TriggerGroupName + "_UPDATE",
                IntervalSeconds = testCase.IntervalSeconds + 10
            };

            var msgCode = _TriggerApi.UpdateAsync(cmd).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test, Order(100)]
        public void Delete()
        {
            var testCase = CreateTestCase();
            testCase.TriggerName += "_UPDATE";
            testCase.TriggerGroupName += "_UPDATE";

            var msgCode = _TriggerApi.DeleteAsync(new
            {
                Name = testCase.TriggerName,
                GroupName = testCase.TriggerGroupName
            }).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }
    }
}
