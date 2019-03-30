using Application.Interfaces.Services;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Models;
using System;
using System.Linq;

namespace Infrastructure.Services
{
    public class PriceAnalyzer : IPriceAnalyzer
    {
        private readonly IPriceEstimateRepository _priceEstimateRepository;
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IUberAPI _uberAPI;

        private Location startLocation;
        private Location endLocation;

        private DayOfWeek[] weekdays; // Verificar porque o parâmetro é null
        private TimeSpan timeFrom;
        private DateTime dateFrom;
        private TimeSpan timeTo;
        private DateTime dateTo;
        private TimeSpan every;
        private DateTime now;

        private double Milliseconds(DateTime date)
        {
            return (new TimeSpan(date.Ticks)).TotalMilliseconds;
        }

        // Time Calculation

        private TimeSpan TimeToWaitForNextCheck()
        {
            var _now = now.TimeOfDay.TotalMilliseconds;
            var _from = timeFrom.TotalMilliseconds;
            var _every = every.TotalMilliseconds;

            var passed = _now - _from;
            var sinceLast = passed % _every;
            var wait = _every - sinceLast;

            return TimeSpan.FromMilliseconds(wait);
        }

        private TimeSpan TimeToWaitForStart()
        {
            var wait 
                = timeFrom.TotalMilliseconds - now.TimeOfDay.TotalMilliseconds;
            return TimeSpan.FromMilliseconds(wait);
        }

        // Waiters

        private async Task WaitForStart()
        {
            if (now.TimeOfDay < timeFrom)
            {
                Console.WriteLine("Still haven't started...");
                var wait = TimeToWaitForStart();

                Console.WriteLine($"Wait for {wait}");
                await Task.Delay(wait);
            }
        }

        private async Task<bool> WaitForNextCheck()
        {
            var wait = TimeToWaitForNextCheck();
            if (now.TimeOfDay + wait >= timeTo) return false;
            await Task.Delay(wait);
            return true;
        }

        // Loaders

        private Analysis LoadAnalysis(Guid id)
        {
            Analysis analysis = _analysisRepository.Get(id);
            if (analysis == null) return null;

            startLocation = analysis.StartLocation;
            endLocation = analysis.EndLocation;

            weekdays = analysis.TimeFrame.Weekdays;
            dateFrom = analysis.TimeFrame.DateFrom;
            timeFrom = analysis.TimeFrame.TimeFrom;
            dateTo = analysis.TimeFrame.DateTo;
            timeTo = analysis.TimeFrame.TimeTo;
            every = analysis.TimeFrame.Every;
            now = DateTime.Now;

            analysis.Status = Status.InProgress;
            _analysisRepository.Update(analysis);

            return analysis;
        }

        private bool CheckCancellation()
        {
            if (now.Date > dateTo.Date) return false;
            return true;
        }

        private bool CheckDayOfWeek()
        {
            return weekdays.Contains(now.DayOfWeek);
        }

        // Task

        private async Task GetReport(Analysis analysis)
        {
            var result = await _uberAPI.Estimate(startLocation, endLocation);
            foreach (var estimate in result)
            {
                estimate.AnalysisId = analysis.Id;
                _priceEstimateRepository.Add(estimate);
                Console.WriteLine($"{estimate.ProductId}: {estimate.AverageEstimate}");
            }
        }

        public async Task StartAnalysis(Guid id)
        {
            Analysis analysis = LoadAnalysis(id);
            if (analysis == null) return;

            Console.WriteLine($"[TASK {id}] Starting\n" +
            	"Now: " + now + "\n" +
            	"From: " + dateFrom + "\n" +
                "To: " + dateTo + "\n" +
                "Every: " + every);

            if (!CheckCancellation()) return; // TODO Cancel Recurrent Task
            if (!CheckDayOfWeek()) return;

            await WaitForStart();
            now = DateTime.Now;

            while (now.TimeOfDay < timeTo)
            {
                Console.WriteLine($"[TASK {id}] Executing\n" +
                "Now: " + now + "\n" +
                "From: " + dateFrom + "\n" +
                "To: " + dateTo + "\n" +
                "Every: " + every);

                await GetReport(analysis);

                if (!await WaitForNextCheck()) break;
                now = DateTime.Now;
            }
        }

        // Constructor

        public PriceAnalyzer(IUberAPI uberAPI,
            IAnalysisRepository analysisRepository,
            IPriceEstimateRepository priceEstimateRepository)
        {
            _priceEstimateRepository = priceEstimateRepository;
            _analysisRepository = analysisRepository;
            _uberAPI = uberAPI;
        }
    }
}
