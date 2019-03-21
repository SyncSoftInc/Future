#if DEBUG
using Quartz;
using SyncSoft.App.Components;
using SyncSoft.App.Logging;
using SyncSoft.ECP.Quartz;
using System;
using System.Threading.Tasks;

namespace SyncSoft.Future.Logistics.Service
{
    public class TestService : JobBase
    {
        private static readonly Lazy<ILogger> _lazyLogger = ObjectContainer.LazyResolveLogger<TestService>();
        protected override ILogger _Logger => _lazyLogger.Value;

        protected override Task<string> InnerExecuteAsync(IJobExecutionContext context)
        {
            var jobId = context.Get("JobId");
            _Logger.Debug("{0} {1}", jobId, DateTime.Now);
            return Task.FromResult(MsgCODES.SUCCESS);
        }
    }
}
#endif