﻿using System;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace AbschlussKonzertKadetten
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();

            var host = Configuration["vcap:services:mariadbent:0:credentials:host"];
            var port = Configuration["vcap:services:mariadbent:0:credentials:port"];
            var db = Configuration["vcap:services:mariadbent:0:credentials:database"];
            var user = Configuration["vcap:services:mariadbent:0:credentials:username"];
            var password = Configuration["vcap:services:mariadbent:0:credentials:password"];
            var connectionString = $"Server={host};UID={user};PWD={password};Database={db};Port={port};";

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContextPool<KadettenContext>(
                options => options.UseMySql(/*connectionString*/ "server=127.0.0.1;port=3306;uid=root;password=gibbiX12345;database=test",
                    mysqlOptions =>
                    {
                        mysqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql);
                    }
                ));
            //services.BuildServiceProvider().GetService<KadettenContext>().Database.Migrate();

            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IClientRepo, ClientRepo>();
            services.AddTransient<ITicketOrderRepo, TicketOrderRepo>();
            services.AddTransient<ITicketRepo, TicketRepo>();
            services.AddTransient<IKadettRepo, KadettRepo>();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, KadettenContext kc)
        {
            kc.Database.EnsureDeleted();
            //kc.Database.EnsureCreated();

            app.UseDeveloperExceptionPage();
            //    app.UseHsts();

            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default_route",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "order", action = "Get" });
            });
        }
    }
}
