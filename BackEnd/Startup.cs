using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd
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
            services.AddCors();
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionStrings"));

            //services.AddCors(options => options.AddPolicy("AllowWebApp",
            //                    builder => builder.AllowAnyOrigin()
            //                                .AllowAnyHeader()
            //                                .AllowAnyMethod()));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            { 
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BackEnd del API",
                Version = "v1"
            });
                c.CustomSchemaIds(type => $"{type.Name}_{System.Guid.NewGuid()}");
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(options =>
                {
                    options.WithOrigins("*");
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api BackEnd v1");
                    c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
                    c.ConfigObject.AdditionalItems.Add("theme", "agate");
                });

            }
            else
            {
                app.UseCors(options =>
                {
                    options.WithOrigins("*");
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
            }
            //app.UseCors("AllowWebApp");

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
