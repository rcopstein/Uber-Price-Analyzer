using System;
using Hangfire;
using Application.Interfaces.Services;

namespace Infrastructure.Services
{
    public class BackgroundTaskManager : IBackgroundTaskManager
    {
        private readonly IPriceAnalyzer _priceAnalyzer;

        public void InitializeTaskForAnalysis(Guid id)
        {
            BackgroundJob.Enqueue(
                () => _priceAnalyzer.StartAnalysis(id));
        }

        public BackgroundTaskManager(IPriceAnalyzer priceAnalyzer)
        {
            _priceAnalyzer = priceAnalyzer;
        }
    }
}
