using SyncSoft.App;
using SyncSoft.ECP.AspNetCore.Hosting;

namespace SyncSoft.Future.Logistics.WebApi
{
    public class Program
    {
        public static string PROJECT = "logistcapi";
        public static void Main(string[] args)
        {
            FutureEngine.Init(PROJECT, args)
                .UseMessageQueue()
                .UseRabbitMQ()
                .UseLogisticsRedis()
                .UseConfigurations()
                .Start();

            ECPHost.Run<Startup>(args);
        }
    }
}
