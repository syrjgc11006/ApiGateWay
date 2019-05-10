using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API02.filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.JwtAuthorize;
using Swashbuckle.AspNetCore.Swagger;

namespace API02
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
            services.AddApiJwtAuthorize((context) =>
            {
                return true;
            });


            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                options.SwaggerDoc("API02", new Info { Title = "API02", Version = "v1", Contact = new Contact { Email = "285130205@qq.com", Name = "API02", Url = "http://0.0.0.0" }, Description = "API02项目" });
                var basePath = ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "API02.xml");
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "请输入带有Bearer的Token", Name = "Authorization", Type = "apiKey" });
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    {
                        "Bearer",
                        Enumerable.Empty<string>()
                    }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc()
                  .UseSwagger(options =>
                  {
                      options.RouteTemplate = "{documentName}/swagger.json";
                  })
                  .UseSwaggerUI(options =>
                  {
                      options.SwaggerEndpoint("/API02/swagger.json", "API02");
                  });
        }
    }
}
