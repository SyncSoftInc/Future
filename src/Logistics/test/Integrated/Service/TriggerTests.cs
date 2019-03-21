using SyncSoft.Future.NUnit.ServiceTest;

namespace SyncSoft.Future.Logistics.IntegratedTest.Service
{
    public class TriggerTests : SyncSoft.Future.NUnit.ServiceTest.TriggerTests
    {
        protected override ServiceTestCase CreateTestCase()
        {
            return new ServiceTestCase
            {
                JobType = "SyncSoft.Future.Logistics.Service.Inventory.ClearOrderHeldInventory, SyncSoft.Future.Logistics.Service",
                JobName = "TEST_SERVICE",
                JobGroupName = "TEST_JOB_GROUP",
                TriggerGroupName = "TEST_TRIGGER_GROUP_2",
                TriggerName = "TEST_TRIGGER_2",
                IntervalSeconds = 10,
                ReplaceExists = true
            };
        }
    }
}
