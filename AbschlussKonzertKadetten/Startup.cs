using System;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace AbschlussKonzertKadetten
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var host = Configuration["vcap:services:mariadbent:0:credentials:host"];
            var port = Configuration["vcap:services:mariadbent:0:credentials:port"];
            var db = Configuration["vcap:services:mariadbent:0:credentials:database"];
            var user = Configuration["vcap:services:mariadbent:0:credentials:username"];
            var password = Configuration["vcap:services:mariadbent:0:credentials:password"];
            var connectionString = $"Server={host};UID={user};PWD={password};Database={db};Port={port};";

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddDbContextPool<KadettenContext>(
            //    options => options.UseMySql(connectionString,
            //        mysqlOptions =>
            //        {
            //            mysqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql);
            //        }
            //    ));
            //services.BuildServiceProvider().GetService<KadettenContext>().Database.Migrate();

            services.AddDbContext<KadettenContext>(options =>
                options.UseMySql("server=localhost;port=3306;uid=root@localhost;password=gibbiX12345"));

            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IClientRepo, ClientRepo>();
            services.AddTransient<ITicketOrderRepo, TicketOrderRepo>();
            services.AddTransient<ITicketRepo, TicketRepo>();
            services.AddTransient<IKadettRepo, KadettRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            //    app.UseHsts();

            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Ticket}/{action=Get}/{id?}");
            });
        }
    }
}
