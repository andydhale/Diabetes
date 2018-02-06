using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diabetes.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Diabetes.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DiabetesContext>(options =>
            {
                if (_env.IsDevelopment() || string.IsNullOrEmpty(_config.GetConnectionString("Diabetes")))
                {
                    options.UseInMemoryDatabase("Diabetes");
                }
                else
                {
                    options.UseSqlServer(_config.GetConnectionString("Diabetes"));
                }
            })
                .AddAutoMapper()
                .AddMvc();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Diabetes API", Version = "1.0.0" });
                options.DescribeAllEnumsAsStrings();

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Diabetes.Web.xml");
                options.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Diabetes API v1");
                })
                .UseStaticFiles()
                .UseMvc();
        }
    }
}
