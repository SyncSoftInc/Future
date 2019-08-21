using SyncSoft.App;
using SyncSoft.ECP.Quartz.Hosting;

namespace SyncSoft.Future.Logistics.Service
{
    public class Program
    {
        public static string PROJECT = "logistcsvc";

        public static void Main(string[] args)
        {
            FutureEngine.Init(PROJECT, args)
                .UseApiClient()
                .UseMessageQueue()
                .UseRabbitMQ()
                .UseQuartz()
                .UseLogisticsRedis()
                .UseLogisticsDomain()
                .UseLogisticsMySql()
                .UseConfigurations()
                .Start();

            QuartzHost.Run<Startup>(args);
        }
    }
}
