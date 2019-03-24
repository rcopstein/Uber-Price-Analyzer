﻿using Application.Interfaces.Services;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Models;
using System;

namespace Infrastructure.Services
{
    public class PriceAnalyzer : IPriceAnalyzer
    {
        private readonly IPriceEstimateRepository _priceEstimateRepository;
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IUberAPI _uberAPI;

        private Location startLocation;
        private Location endLocation;

        private TimeSpan every;
        private DateTime from;
        private DateTime now;
        private DateTime to;

        private double Milliseconds(DateTime date)
        {
            return (new TimeSpan(date.Ticks)).TotalMilliseconds;
        }

        // Time Calculation

        private TimeSpan TimeToWaitForNextCheck()
        {
            var _now = Milliseconds(now);
            var _from = Milliseconds(from);
            var _every = every.TotalMilliseconds;

            var passed = _now - _from;
            var sinceLast = passed % _every;
            var wait = _every - sinceLast;

            return TimeSpan.FromMilliseconds(wait);
        }

        private TimeSpan TimeToWaitForStart()
        {
            var wait = Milliseconds(from) - Milliseconds(now);
            return TimeSpan.FromMilliseconds(wait);
        }

        // Waiters

        private async Task WaitForStart()
        {
            if (now < from)
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
            if (now + wait >= to) return false;
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

            every = analysis.TimeFrame.Every;
            from = analysis.TimeFrame.From;
            to = analysis.TimeFrame.To;
            now = DateTime.Now;

            analysis.Status = Status.InProgress;
            _analysisRepository.Update(analysis);

            return analysis;
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
            	"From: " + from + "\n" +
                "To: " + to + "\n" +
                "Every: " + every);

            await WaitForStart();
            now = DateTime.Now;

            while (now < to)
            {
                Console.WriteLine($"[TASK {id}] Executing\n" +
                "Now: " + now + "\n" +
                "From: " + from + "\n" +
                "To: " + to + "\n" +
                "Every: " + every);

                await GetReport(analysis);

                if (!await WaitForNextCheck()) break;
                now = DateTime.Now;
            }

            analysis.Status = Status.Completed;
            _analysisRepository.Update(analysis);

            Console.WriteLine($"[TASK {id}] Finished");
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
