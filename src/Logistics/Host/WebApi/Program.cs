using SyncSoft.App;
using SyncSoft.ECP.AspNetCore.Hosting;

namespace SyncSoft.Future.Logistics.WebApi
{
    public class Program
    {
        public static string PROJECT = "logisticsapi";
        public static void Main(string[] args)
        {
            FutureEngine.Init(PROJECT, args)
                .UseMessageQueue()
                .UseRabbitMQ()
                .UseLogisticsRedis()
                .UseLogisticsMySql()
                .UseLogisticsDF()
                .UseJsonConfiguration()
                .Start();

            ECPHost.Run<Startup>(args);
        }
    }
}
