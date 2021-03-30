using System;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Handler;
using AbschlussKonzertKadetten.Interface;
using AbschlussKonzertKadetten.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AbschlussKonzertKadetten
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();
            var connectionString = "Server=sql03.popnetinf.local;UID=kadetten-thun;PWD=5&GrA-2c!Xd@;Database=kadetten-thun;Port=3306;";
            if (HostingEnvironment.IsDevelopment())
            {
                connectionString =
                    "server = 127.0.0.1; port = 3306; uid = root; password = gibbiX12345; database = test";
                //"server = localhost; port = 63306; uid = gJjPnRlt9zRoSm4y; password = jh4gOJZmFhlaMnIG; database = CFDB_F5CF1261_22B8_45F4_B8A2_3BA63103BFAD";
            }

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContextPool<KadettenContext>(
                options => options.UseMySql(connectionString,
                    mysqlOptions => { mysqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql); }
                    //add assembly
                ));
            //Type of startup.getTypeinfo.assembly.getname() name

            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IClientRepo, ClientRepo>();
            services.AddTransient<ITicketOrderRepo, TicketOrderRepo>();
            services.AddTransient<ITicketRepo, TicketRepo>();
            services.AddTransient<IKadettRepo, KadettRepo>();
            services.AddTransient<IRedactorRepo, RedactorRepo>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IFormularActiveRepo, FormularActiveRepo>();
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
            app.UseDeveloperExceptionPage();
            //kc.Database.Migrate();
            kc.Database.EnsureCreated();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=order}/{action=Get}/{id?}"); });

        }
    }
}
