using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.JwtAuthorize;
using Ocelot.Middleware;

namespace ApiGateWay
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
            services.AddOcelotJwtAuthorize();
            //注入ocelot
            services.AddOcelot(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //注入swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("ApiGateWay", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Api网关服务",
                    Version = "V1",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "SwaggerOcelot",
                        Url = "",
                        Email = "285130205@qq.com"
                    },
                    Description = "网关平台"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

            /// <summary>
            ///【谨记】 该方法一直会报api文档无法加载的错误
            /// </summary>
            /// <param name="app"></param>
            /// <param name="env"></param>
        //public  void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    var apis = new Dictionary<string, string>(
        //       new KeyValuePair<string, string>[] {
        //            KeyValuePair.Create("SwaggerAuthorize", "Authorize"),
        //            KeyValuePair.Create("SwaggerAPI01", "API01"),
        //            KeyValuePair.Create("SwaggerAPI02", "API02")
        //       });

        //    app.UseMvc()
        //       .UseSwagger()
        //       .UseSwaggerUI(options =>
        //       {
        //           apis.Keys.ToList().ForEach(key =>
        //           {
        //               options.SwaggerEndpoint($"/{key}/swagger.json", $"{apis[key]} -【{key}】");
        //           });
        //           options.DocumentTitle = "Swagger测试平台";
        //       });
        //     app.UseOcelot();
        //}
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var apis = new Dictionary<string, string>(
               new KeyValuePair<string, string>[] {
                    KeyValuePair.Create("ApiAuthorize", "Authorize"),
                    KeyValuePair.Create("API01", "API01"),
                    KeyValuePair.Create("API02", "API02")
               });

            app.UseMvc()
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                   apis.Keys.ToList().ForEach(key =>
                   {
                       options.SwaggerEndpoint($"/{key}/swagger.json", $"{apis[key]} -【{key}】");
                   });
                   options.DocumentTitle = "Swagger测试平台";
               });
            await app.UseOcelot();
        }
    }
}
