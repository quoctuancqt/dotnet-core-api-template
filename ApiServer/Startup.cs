using JwtTokenServer.Extensions;
using JwtTokenServer.Proxies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Mapper.AutoMapperConfiguration.Config();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Config Swashbuckle
            //services.AddSwashbuckle();

            services.AddSingleton(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<Core.Context>(options => options.UseSqlite("Data Source=SampleDb.db"));

            services.JWTAddAuthentication(Configuration);

            services.AddHttpClient<OAuthClient>(typeof(OAuthClient).Name, client => client.BaseAddress = new Uri("http://localhost:5000"));

            //Config DI
            //services.AddServices();

            //Config FluentValidation
            //services.AddValidators();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Config Swashbuckle
            //app.UseSwashbuckle();

            //Config Log
            //app.AddLog(Configuration, env);

            app.JWTBearerToken(Configuration);

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
