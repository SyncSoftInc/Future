using SyncSoft.App;
using SyncSoft.ECP.AspNetCore.Hosting;

namespace SyncSoft.Future.Password.WebApi
{
    public class Program
    {
        public static string PROJECT = "passport";
        public static void Main(string[] args)
        {
            FutureEngine.Init(PROJECT, args)
                .UseFutureRedis()
                .UseMongoDBGrantStore(options => options.GrantStoreConnStrName = "MONGO_PASSPORT")
                .UsePassportDomain()
                .UsePassportMySql()
                .UseConfigurations()
                .Start();

            ECPHost.Run<Startup>(args);
        }
    }
}
