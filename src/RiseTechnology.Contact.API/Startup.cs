using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Contact.API.Context;
using RiseTechnology.Contact.API.MapProfile;
using RiseTechnology.Contact.API.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseTechnology.Contact.API
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

            services.AddControllers();
            services.AddDbContext<ContactContext>(option => option.UseSqlServer(Configuration.GetConnectionString("RiseContactSqlServer")));

            //Auto Register Servise Lifetime
            services.Scan(scan => scan
                .FromAssemblyOf<Startup>()
                .AddClasses(classes => classes.AssignableTo<ITransientLifetime>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<IScopedLifetime>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonLifetime>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            services.AddSingleton(new MapperConfiguration(x =>
            {
                x.AddProfile(new ContactApiMapperProfile());
            }).CreateMapper());
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RiseTechnology.Contact.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                 app.SeedContactContext();  //Uygulama geliþtirme aþamasýnda çaðýrýyoruz. 
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RiseTechnology.Contact.API v1"));
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
