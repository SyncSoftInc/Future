using SyncSoft.ECP.Quartz.Hosting;

namespace SyncSoft.Future.Product.Service
{
    public class Program
    {
        public static void Main(string[] args) => QuartzHost.Run<Startup>(args);
    }
}
