using Hangfire;
using Infrastructure.Data;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Application.Interfaces.UseCases;
using Application.Interfaces.Repositories;
using Application.UseCases.GetAllAnalyses;
using Application.UseCases.IncludeAnalysis;
using Microsoft.AspNetCore.Diagnostics;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;

namespace API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<Database>(opt => opt.UseInMemoryDatabase("Database")); 
            services.AddHangfire(x => x.UseMemoryStorage());
            services.AddHttpClient();

            // Services
            services.AddScoped<IIncludeAnalysis, IncludeAnalysis>();
            services.AddScoped<IGetAllAnalyses, GetAllAnalyses>();
            services.AddScoped<DbContext, Database>();

            // Repositories
            services.AddScoped<IAnalysisRepository, AnalysisRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseHsts();

            app.UseExceptionHandler(handler =>
            {
                handler.Run(async ctx =>
                {
                    var exceptionHandlerPathFeature =
                        ctx.Features.Get<IExceptionHandlerPathFeature>();

                    var error = exceptionHandlerPathFeature?.Error;

                    if (error is ValidationException)
                    {
                        ctx.Response.StatusCode = 400;
                        ctx.Response.ContentType = "application/json";

                        using (var writer = new StreamWriter(ctx.Response.Body))
                        {
                            new JsonSerializer().Serialize(
                                writer,
                                ((ValidationException)error).Summary);
                            await writer.FlushAsync().ConfigureAwait(false);
                        }
                    }

                    else
                    {
                        ctx.Response.StatusCode = 500;
                    }

                });
            });
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseHangfireServer();
            app.UseHangfireDashboard();

        }
    }
}
