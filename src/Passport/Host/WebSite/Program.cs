using SyncSoft.ECP.AspNetCore.Hosting;

namespace SyncSoft.Future.Passport
{
    public class Program
    {
        public static void Main(string[] args) => ECPHost.Run<Startup>(args);
    }
}
