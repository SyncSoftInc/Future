using SyncSoft.Future.NUnit.ServiceTest;

namespace SyncSoft.Future.Logistics.IntegratedTest.Service
{
    public class JobTests : SyncSoft.Future.NUnit.ServiceTest.JobTests
    {
        protected override JobTestCase CreateTestCase()
        {
            return new JobTestCase
            {
                JobType = "SyncSoft.Future.Logistics.Service.Inventory.ClearOrderHeldInventory, SyncSoft.Future.Logistics.Service",
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
