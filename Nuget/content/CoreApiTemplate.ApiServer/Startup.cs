﻿using Application.Services;
using Core.Extensions;
using Core.Middlewares;
using Domain.Identities;
using FluentValidation.AspNetCore;
using JwtTokenServer.Extensions;
using JwtTokenServer.Proxies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System;

namespace ApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            //Config Swashbuckle
            services.AddSwashbuckle();

            services.AddSingleton(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=Demo.db"));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            //Config OAuth Server
            services.JWTAddAuthentication();

            services.AddHttpClient<OAuthClient>(typeof(OAuthClient).Name, client => client.BaseAddress = new Uri("http://localhost:5000"));

            services.AddAccountManager<AccountManager>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Config DI
            services.AddServices();

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddFluentValidation(fv => fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            //Seed data
            DbSeed.SeedAsync(app).GetAwaiter().GetResult();

            //Config Swashbuckle
            app.UseSwashbuckle();

            //Config handle global error
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();

            app.JWTBearerToken(Configuration);

            app.UseMvc();
        }
    }
}