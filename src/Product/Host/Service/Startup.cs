using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SyncSoft.App;
using SyncSoft.ECP.AspNetCore.Hosting;


namespace SyncSoft.Future.Product.Service
{
    public class Startup : SerilogStartup
    {
        private const string RESOURE_NAME = "productapi";

        public Startup(IConfiguration configuration)
            : base(configuration)
        {
            HostEngine.Init(configuration, RESOURE_NAME)
                .UseQuartz()
                .UseProductDomain()
                .UseProductRedis()
                .UseProductMySql()
                .Start();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiServer(RESOURE_NAME);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseReverseProxy();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseApiServer(RESOURE_NAME);
        }
    }
}
