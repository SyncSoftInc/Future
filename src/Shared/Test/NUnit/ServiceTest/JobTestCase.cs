using System;
using System.Collections.Generic;
using System.Text;

namespace SyncSoft.Future.NUnit.ServiceTest
{
    public class JobTestCase
    {
        public string JobName { get; set; }
        public string JobType { get; set; }
        public string JobGroupName { get; set; }
        public string TriggerName { get; set; }
        public string TriggerGroupName { get; set; }
        public string CronExpression { get; set; }
        public int IntervalSeconds { get; set; }
        public bool ReplaceExists { get; set; }
    }
}
