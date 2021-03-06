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
using DBI_Apotheke.Core.Workloads.ProductInfos;
using DBI_Apotheke.Core.Workloads.Products;
using DBI_Apotheke.Core.Workloads.Storages;
using DBI_Apotheke.Core.Workloads.Recipes;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Driver;

namespace App
{
    public class Startup
    {
        private const string ORIGIN = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        private async Task SetIndexes(IDatabaseProvider databaseProvider)
        {
            var session = await databaseProvider.StartSession();
            
            
            var collection = databaseProvider.Database.GetCollection<Product>(MongoUtil.GetCollectionName<Product>());
            var options = new CreateIndexOptions() {Unique = true};
            
            var indexKeysDefinition = Builders<Product>.IndexKeys.Ascending(p => p.PZN);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<Product>(indexKeysDefinition, options));

            await session.CommitTransactionAsync();
        }
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection(AppSettings.KEY));
            services.AddAutoMapper(typeof(MapperProfile));

            // configure fwk
            services.AddLeoMongo<MongoConfig>();

            services.AddScoped<IProductInfoRepository, ProductInfoRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();

            services.AddScoped<IProductInfoService, ProductInfoService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IRecipeService, RecipeService>();

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDatabaseProvider db)
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

            SetIndexes(db).ConfigureAwait(false);
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