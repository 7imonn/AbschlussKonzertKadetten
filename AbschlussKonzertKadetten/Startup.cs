using System;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            var host = Configuration["VCAP_SERVICES:mariadbent:0:credentials:host"];
            var port = Configuration["VCAP_SERVICES:mariadbent:0:credentials:port"];
            var db = Configuration["VCAP_SERVICES:mariadbent:0:credentials:database"];
            var user = Configuration["VCAP_SERVICES:mariadbent:0:credentials:username"];
            var password = Configuration["VCAP_SERVICES:mariadbent:0:credentials:password"];
            var connectionString = $"Server={host};UID={user};PWD={password};Database={db};Port={port};";

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContextPool<KadettenContext>( // replace "YourDbContext" with the class name of your DbContext
                options => options.UseMySql(connectionString,
                    mysqlOptions =>
                    {
                        mysqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql); // replace with your Server Version and Type
                    }
                ));
            services.BuildServiceProvider().GetService<KadettenContext>().Database.Migrate();

            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IClientRepo, ClientRepo>();
            services.AddTransient<ITicketOrderRepo, TicketOrderRepo>();
            services.AddTransient<ITicketRepo, TicketRepo>();
            services.AddTransient<IKadettRepo, KadettRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials(); });
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Ticket}/{action=Get}/{id?}");
            });
        }
    }
}
