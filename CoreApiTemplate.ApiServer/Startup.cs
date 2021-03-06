﻿using CoreApiTemplate.Application;
using CoreApiTemplate.Application.Services;
using CoreApiTemplate.Core.Extensions;
using CoreApiTemplate.Core.Middlewares;
using CoreApiTemplate.Domain.Identities;
using CoreApiTemplate.Dto;
using CoreApiTemplate.Persistence;
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
using System;

namespace CoreApiTemplate.ApiServer
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

            services.AddDbContext<ApplicationContext>(options =>
            {
                if (Configuration.GetSection("EfSettings").GetValue<string>("EnableRetry") == bool.TrueString)
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase"), sqlOptions =>
                    {
                        var retryCount = int.Parse(Configuration.GetSection("EfSettings").GetValue<string>("SqlRetryCount"));

                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: retryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
                }
                else
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase"));
                }
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            //Config OAuth Server
            services.JWTAddAuthentication();

            services.AddHttpClient<OAuthClient>(typeof(OAuthClient).Name, client => client.BaseAddress = new Uri("http://localhost:5000"));

            services.AddAccountManager<AccountManager>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Config DI
            services.AddServices(Configuration);

            services.AddCustomServices();

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddFluentValidation(fv =>
                    {
                        fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                        fv.RegisterValidatorsFromAssemblyContaining<LoginDtoValidator>();
                    });
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
