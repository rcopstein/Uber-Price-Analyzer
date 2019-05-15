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
using Application.UseCases.GetAnalysis;
using Application.Interfaces.Services;
using Infrastructure.Services;
using Infrastructure.Services.UberAPI;
using Application.UseCases.GenerateReport;
using Microsoft.AspNetCore.HttpOverrides;

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
            // services.AddDbContext<Database>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHangfire(x => x.UseMemoryStorage());
            services.AddDbContext<Database>();
            services.AddHttpClient();

            // Use Cases
            services.AddScoped<IIncludeAnalysis, IncludeAnalysis>();
            services.AddScoped<IGenerateReport, GenerateReport>();
            services.AddScoped<IGetAllAnalyses, GetAllAnalyses>();
            services.AddScoped<IGetAnalysis, GetAnalysis>();

            // Services
            services.AddScoped<IBackgroundTaskManager, BackgroundTaskManager>();
            services.AddScoped<IPriceAnalyzer, PriceAnalyzer>();
            services.AddScoped<IUberAPI, UberAPI>();

            // Data
            services.AddScoped<IPriceEstimateRepository, PriceEstimateRepository>();
            services.AddScoped<IAnalysisRepository, AnalysisRepository>();
            services.AddScoped<DbContext, Database>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else app.UseHsts();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
