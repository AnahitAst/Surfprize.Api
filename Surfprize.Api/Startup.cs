
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Surfprize.Api.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace Surfprize.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(environment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDb(Configuration);
            services.ConfigureServices();
            services.ConfigureOptions(Configuration);
            services.ConfigureAutoMapper();
            services.ConfigureApi();
            services.ConfigureJwtToken();


            //services.AddMvc(options =>
            //{
            //    options.EnableEndpointRouting = false;
            //}).AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //});          

            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvcWithDefaultRoute();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wedding Builder V1");
            });
        }
    }
}
