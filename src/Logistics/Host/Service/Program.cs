using SyncSoft.App;
using SyncSoft.ECP.Quartz.Hosting;

namespace SyncSoft.Future.Logistics.Service
{
    public class Program
    {
        public static string PROJECT = "logisticssvc";

        public static void Main(string[] args)
        {
            FutureEngine.Init(PROJECT, args)
                .UseApiClient()
                .UseMessageQueue()
                .UseRabbitMQ()
                .UseLogisticsRedis()
                .UseLogisticsDomain()
                .UseLogisticsMySql()
                .UseJsonConfiguration()
                .Start();

            QuartzHost.Run<Startup>(args);
        }
    }
}
