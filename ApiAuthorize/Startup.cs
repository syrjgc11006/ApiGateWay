using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.JwtAuthorize;
using Swashbuckle.AspNetCore.Swagger;

namespace ApiAuthorize
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
            services.AddTokenJwtAuthorize();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("ApiAuthorize",
                    new Swashbuckle.AspNetCore.Swagger.Info
                    {
                        Title = "ApiAuthorize",
                        Version = "v1",
                        Contact = new Contact
                        { Email = "285130205@qq.com", Name = "Authorize", Url = "http://0.0.0.0" },
                        Description = "Authorize项目"
                    });
                var basePath = ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ApiAuthorize.xml");
                option.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "{documentName}/swagger.json";
            })
            .UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/ApiAuthorize/swagger.json", "Authorize");
            }
                );
        }
    }
}
