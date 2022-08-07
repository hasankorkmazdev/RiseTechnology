using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RiseTechnology.Common.Constant;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Common.GenericRepository;
using RiseTechnology.Report.API.Context;
using System;

namespace RiseTechnology.Report.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RiseTechnology.Report.API", Version = "v1" });
            });
            //UoW Generic Yap�land�r�ld��� i�in DBContext Kullan�r E�er DBContexti �a��r�rsam Ondan T�retilip Kullan�lan� Ver
            services.AddScoped<DbContext, ReportContext>();

            //Auto Register Servise Lifetime
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(Startup), typeof(UnitOfWork)) // Bu Assemblyler i�inde ITransientLifetime Transiet,IScopedLifetime Scoped, ISingletonLifetime Singleton olarak otomatik Implemente Et
                .AddClasses(classes => classes.AssignableTo<ITransientLifetime>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<IScopedLifetime>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonLifetime>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddHttpClient(ReportApiConstants.ContactAPIClient, httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration.GetValue<string>("Services:ContactServiceEndPoint"));
                httpClient.Timeout = TimeSpan.FromSeconds(30);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RiseTechnology.Report.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
