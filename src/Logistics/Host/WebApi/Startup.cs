﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SyncSoft.App;
using SyncSoft.ECP.AspNetCore.Hosting;

namespace SyncSoft.Future.Logistics.WebApi
{
    public class Startup : SerilogStartup
    {
        private const string RESOURE_NAME = "logisticsapi";

        public Startup(IConfiguration configuration)
            : base(configuration)
        {
            HostEngine.Init(configuration, RESOURE_NAME)
                .UseLogisticsRedis()
                .UseLogisticsMySql()
                .UseLogisticsDF()
                .Start();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiServer(RESOURE_NAME);
        }

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
