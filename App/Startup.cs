using System;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DBI_Apotheke.Core.Util;
namespace App
{
    public class Startup
    {
        private const string ORIGIN = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection(AppSettings.KEY));
            services.AddAutoMapper(typeof(MapperProfile));

            // configure fwk
            services.AddLeoMongo<MongoConfig>();

            // TODO Add services
            //services.AddScoped<IPostRepository, PostRepository>();
            

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            services.AddControllers();

            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy(ORIGIN,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5000",
                                "http://localhost:4200") // Angular CLI
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); });
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseMiddleware<ErrorLoggingMiddleware>();

            app.UseRouting();

            app.UseCors(ORIGIN);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public sealed class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}