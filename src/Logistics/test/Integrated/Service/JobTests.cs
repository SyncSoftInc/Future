using SyncSoft.Future.NUnit.ServiceTest;

namespace SyncSoft.Future.Logistics.IntegratedTest.Service
{
    public class JobTests : SyncSoft.Future.NUnit.ServiceTest.JobTests
    {
        protected override ServiceTestCase CreateTestCase()
        {
            return new ServiceTestCase
            {
                JobType = "SyncSoft.Future.Logistics.Service.TestService, SyncSoft.Future.Logistics.Service",
                JobName = "TEST_SERVICE",
                JobGroupName = "TEST_JOB_GROUP",
                TriggerGroupName = "TEST_TRIGGER_GROUP",
                TriggerName = "TEST_TRIGGER",
                IntervalSeconds = 10,
                ReplaceExists = true
            };
        }
    }
}
