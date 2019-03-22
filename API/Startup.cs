using Hangfire;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Application.Interfaces.UseCases;
using Domain.Repositories;
using Application.UseCases.GetAllAnalyses;
using Application.UseCases.IncludeAnalysis;
using Microsoft.AspNetCore.Diagnostics;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using Application.UseCases.GetAnalysis;
using Application.Interfaces.Services;
using Infrastructure.Services;

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

            // Use Cases
            services.AddScoped<IIncludeAnalysis, IncludeAnalysis>();
            services.AddScoped<IGetAllAnalyses, GetAllAnalyses>();
            services.AddScoped<IGetAnalysis, GetAnalysis>();

            // Services
            services.AddScoped<IBackgroundTaskManager, BackgroundTaskManager>();
            services.AddScoped<IPriceAnalyzer, PriceAnalyzer>();
            services.AddScoped<IUberAPI, UberAPI>();

            // Data
            services.AddScoped<IAnalysisRepository, AnalysisRepository>();
            services.AddScoped<DbContext, Database>();
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
                        await ctx.Response.WriteAsync(error.ToString());
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
