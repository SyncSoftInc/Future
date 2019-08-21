using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SyncSoft.ECP.AspNetCore.Hosting;

namespace SyncSoft.Future.Logistics.Service
{
    public class Startup : SerilogStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiServer(Program.PROJECT);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseReverseProxy();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseApiServer(Program.PROJECT);
        }
    }
}
