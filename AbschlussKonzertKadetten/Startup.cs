﻿using System;
using AbschlussKonzertKadetten.Context;
using AbschlussKonzertKadetten.Interface;
using AbschlussKonzertKadetten.Middelware;
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
            var connectionString = String.Empty;
            if (HostingEnvironment.IsDevelopment())
            {
                connectionString =
                    "server = 127.0.0.1; port = 3306; uid = root; password = gibbiX12345; database = test";

            }
            else if (HostingEnvironment.IsStaging())
            {
                var host = Configuration["vcap:services:mariadbent:0:credentials:host"];
                var port = Configuration["vcap:services:mariadbent:0:credentials:port"];
                var db = Configuration["vcap:services:mariadbent:0:credentials:database"];
                var user = Configuration["vcap:services:mariadbent:0:credentials:username"];
                var password = Configuration["vcap:services:mariadbent:0:credentials:password"];
                connectionString = $"Server={host};UID={user};PWD={password};Database={db};Port={port};";
            }
            else
            {
                connectionString = "Server=sql03.popnetinf.local;UID=kadetten-thun;PWD=5&GrA-2c!Xd@;Database=kadetten-thun;Port=3306;";
            }

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContextPool<KadettenContext>(
                options => options.UseMySql(connectionString,
                    mysqlOptions => { mysqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql); }
                ));

            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IClientRepo, ClientRepo>();
            services.AddTransient<ITicketOrderRepo, TicketOrderRepo>();
            services.AddTransient<ITicketRepo, TicketRepo>();
            services.AddTransient<IKadettRepo, KadettRepo>();
            services.AddTransient<IRedactorRepo, RedactorRepo>();
            //services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IFormularActiveRepo, FormularActiveRepo>();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            
                    services.AddTransient<IOrderRepo, OrderRepo>();
                    services.AddTransient<IClientRepo, ClientRepo>();
                    services.AddTransient<ITicketOrderRepo, TicketOrderRepo>();
                    services.AddTransient<ITicketRepo, TicketRepo>();
                    services.AddTransient<IKadettRepo, KadettRepo>();
                    services.AddTransient<IUserRepo, UserRepo>();
                    services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, KadettenContext kc, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //kc.Database.EnsureDeleted();
                app.UseDeveloperExceptionPage();
            }
            if (env.IsStaging())
            {
                //kc.Database.EnsureDeleted();
                app.UseDeveloperExceptionPage();
            }
            kc.Database.EnsureCreated();

            //    app.UseHsts();
            //app.UseMiddleware<AuthenticationMiddleware>();
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=order}/{action=Get}/{id?}"); });

        }
    }
}
