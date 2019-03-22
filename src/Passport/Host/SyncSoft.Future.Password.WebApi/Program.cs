using SyncSoft.ECP.AspNetCore.Hosting;

namespace SyncSoft.Future.Password.WebApi
{
    public class Program
    {
        public static void Main(string[] args) => ECPHost.Run<Startup>(args);
    }
}
