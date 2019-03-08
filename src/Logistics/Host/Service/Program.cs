using SyncSoft.ECP.Quartz.Hosting;

namespace SyncSoft.Future.Logistics.Service
{
    public class Program
    {
        public static void Main(string[] args) => QuartzHost.Run<Startup>(args);
    }
}
