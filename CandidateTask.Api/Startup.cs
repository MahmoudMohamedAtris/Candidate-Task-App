using CandidateTask.Application;
using CandidateTask.Infrastructure;
using CandidateTask.Infrastructure.Caching;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CandidateTask.Api
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

            services.AddControllers()
                 .AddFluentValidation(options =>
                 {
                     // Validate child properties and root collection elements
                     options.ImplicitlyValidateChildProperties = true;
                     options.ImplicitlyValidateRootCollectionElements = true;

                     // Automatic registration of validators in assembly
                     options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                 }); 

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CandidateTask.Api", Version = "v1" });
            });

            //IOC
            services.AddMemoryCache();
            services.AddInfrastructure(Configuration);
            services.AddApplication(Configuration);
            services.AddCache(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CandidateTask.Api v1"));
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
