using Hangfire;
using Domain.Models;
using Application.Interfaces.Services;
using System;

namespace Infrastructure.Services
{
    public class BackgroundTaskManager : IBackgroundTaskManager
    {
        private readonly IPriceAnalyzer _priceAnalyzer;

        public void InitializeTaskForAnalysis(Analysis analysis)
        {
            var id = analysis.Id;

            var timeFrom = analysis.TimeFrame.TimeFrom;
            timeFrom -= TimeZoneInfo.Local.BaseUtcOffset;

            var hour = timeFrom.Hours;
            var minute = timeFrom.Minutes;

            RecurringJob.AddOrUpdate(
                id.ToString(),
                () => _priceAnalyzer.StartAnalysis(id),
                $"{minute} {hour} * * *");
        }

        public BackgroundTaskManager(IPriceAnalyzer priceAnalyzer)
        {
            _priceAnalyzer = priceAnalyzer;
        }
    }
}
