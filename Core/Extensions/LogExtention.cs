using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class LogExtention
    {
        public static IApplicationBuilder AddLog(this IApplicationBuilder app,
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            app.UseExceptionHandler(options => {
                options.Run(async context => {

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
                    context.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");

                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    if (exception.Error is Exceptions.BadRequestException)
                    {

                        await BadRequest(context, exception);

                        return;
                    }

                    //TODO add log

                    if (env.IsDevelopment())
                    {
                        context.Response.StatusCode = 500;

                        context.Response.ContentType = "application/json";

                        var error = JsonConvert.SerializeObject(new
                        {
                            error = exception.Error.Message,
                            stackTrace = exception.Error.StackTrace,
                        });

                        await context.Response.WriteAsync(error).ConfigureAwait(false);

                        return;
                    }
                });
            });

            return app;
        }

        private static async Task BadRequest(HttpContext context, IExceptionHandlerFeature exception)
        {
            context.Response.StatusCode = 400;

            context.Response.ContentType = "application/json";

            var badRequestException = exception.Error as Exceptions.BadRequestException;

            if (badRequestException.Errors == null)
            {
                var error = JsonConvert.SerializeObject(new { error = exception.Error.Message });

                await context.Response.WriteAsync(error).ConfigureAwait(false);
            }
            else
            {
                var values = badRequestException.Errors.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key.First().ToString().ToLower() + d.Key.Substring(1), d.Value));

                await context.Response.WriteAsync("{" + string.Join(",", values) + "}").ConfigureAwait(false);
            }
        }

        private static async Task ServerInternal(HttpContext context,
            IExceptionHandlerFeature exception,
            IHostingEnvironment env)
        {
            string error = string.Empty;

            if (env.IsDevelopment())
            {
                error = JsonConvert.SerializeObject(new
                {
                    error = exception.Error.Message,
                    stackTrace = exception.Error.StackTrace,
                });
            }
            else
            {
                error = JsonConvert.SerializeObject(new
                {
                    error = "Something when wrong please contact with administrator thanks",
                });
            }

            context.Response.StatusCode = 500;

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(error).ConfigureAwait(false);

        }
    }
}
