using NUnit.Framework;
using SyncSoft.App.Components;
using SyncSoft.ECP.APIs.Service;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.NUnit.ServiceTest
{
    public abstract class JobTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<IJobApi> _lazyJobApi = ObjectContainer.LazyResolve<IJobApi>();
        private IJobApi _JobApi => _lazyJobApi.Value;

        #endregion

        protected abstract JobTestCase CreateTestCase();

        [Test]
        public void Create()
        {
            var testCase = CreateTestCase();

            var cmd = new
            {
                testCase.JobType,
                testCase.JobName,
                testCase.JobGroupName,
                testCase.TriggerGroupName,
                testCase.TriggerName,
                testCase.IntervalSeconds,
                testCase.ReplaceExists
            };
            var msgCode = _JobApi.CreateAsync(cmd).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test]
        public void Get()
        {
            var testCase = CreateTestCase();
            var rs = _JobApi.GetAsync(new
            {
                Name = testCase.JobName,
                GroupName = testCase.JobGroupName
            }).ResultForTest();

            Assert.IsTrue(rs.IsNotNull());
        }

        [Test]
        public void GetGroups()
        {
            var rs = _JobApi.GetGroupsAsync().ResultForTest();
            Assert.IsTrue(rs.IsNotNull());
        }

        [Test]
        public void GetTriggers()
        {
            var testCase = CreateTestCase();
            var rs = _JobApi.GetTriggersAsync(new
            {
                Name = testCase.JobName,
                GroupName = testCase.JobGroupName
            }).ResultForTest();
            Assert.IsTrue(rs.IsNotNull());
        }

        [Test]
        public void Interrupt()
        {
            var testCase = CreateTestCase();
            var msgCode = _JobApi.InterruptAsync(new
            {
                Name = testCase.JobName,
                GroupName = testCase.JobGroupName
            }).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test]
        public void Pause()
        {
            var testCase = CreateTestCase();
            var msgCode = _JobApi.PauseAsync(new
            {
                Name = testCase.JobName,
                GroupName = testCase.JobGroupName
            }).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test]
        public void Resume()
        {
            var testCase = CreateTestCase();
            var msgCode = _JobApi.ResumeAsync(new
            {
                Name = testCase.JobName,
                GroupName = testCase.JobGroupName
            }).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test]
        public void Trigger()
        {
            var testCase = CreateTestCase();
            var msgCode = _JobApi.TriggerAsync(new
            {
                Name = testCase.JobName,
                GroupName = testCase.JobGroupName
            }).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }

        [Test]
        public void Delete()
        {
            var testCase = CreateTestCase();
            var cmd = new
            {
                testCase.JobName,
                testCase.JobGroupName,
            };

            var msgCode = _JobApi.DeleteAsync(cmd).ResultForTest();
            Assert.IsTrue(msgCode.IsSuccess());
        }
    }
}
