using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SyncSoft.App;
using SyncSoft.ECP.AspNetCore.Hosting;

namespace SyncSoft.Future.Passport
{
    public class Startup : SerilogStartup
    {
        private const string RESOURE_NAME = "passport";

        public Startup(IConfiguration configuration) : base(configuration)
        {
            HostEngine.Init(configuration
                , RESOURE_NAME
                , useRabbitMQ: false                    // 简化Passport部署
                , allowOverridingRegistrations: true    // 重载了IUserProfileProvider
            )
                .UseApiClient()
                .UseMongoDBGrantStore(options => options.GrantStoreConnStrName = "MONGO_PASSPORT")
                .UseFutureRedis()
                .UsePassportDomain()
                .UsePassportMySql()
                .Start();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPassportServer();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseReverseProxy();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UsePassportServer();
        }
    }
}
