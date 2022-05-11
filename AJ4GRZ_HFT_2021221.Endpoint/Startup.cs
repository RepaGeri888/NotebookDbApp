using AJ4GRZ_HFT_2021221.Data;
using AJ4GRZ_HFT_2021221.Logic;
using AJ4GRZ_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotebookDbApp.Endpoint.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace AJ4GRZ_HFT_2021221.Endpoint
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
            services.AddSignalR();
            services.AddTransient<NotebookDbContext>();

            services.AddTransient<IBrandLogic, BrandLogic>();
            services.AddTransient<ICpuLogic, CpuLogic>();
            services.AddTransient<IGpuLogic, GpuLogic>();
            services.AddTransient<INotebookLogic, NotebookLogic>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ICpuRepository, CpuRepository>();
            services.AddTransient<IGpuRepository, GpuRepository>();
            services.AddTransient<INotebookRepository, NotebookRepository>();
            services.AddTransient<NotebookDbContext, NotebookDbContext>();


            services.AddControllers();
            //services.AddControllers().
            //    AddJsonOptions(options =>
            //    {
            //        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //        options.JsonSerializerOptions.MaxDepth = 150;
            //    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotebookDbApp.Endpoint", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotebookDbApp.Endpoint v1"));
            }
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:17514"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
