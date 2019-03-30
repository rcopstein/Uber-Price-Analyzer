using Application.Interfaces.UseCases;
using Domain.Repositories;
using Domain.Models;
using System;

namespace Application.UseCases.GenerateReport
{
    public class GenerateReport : IGenerateReport
    {
        private readonly IAnalysisRepository _repository;

        private Report Generate(Analysis analysis)
        {
            float count = 0;
            float? sum = null;
            float? lowest = null;
            float? highest = null;
            DateTime lowestDate = DateTime.MinValue;
            DateTime highestDate = DateTime.MinValue;

            foreach (var priceEstimate in analysis.Prices)
            {
                var price = priceEstimate.AverageEstimate;

                if (highest == null || price > highest)
                {
                    highest = price;
                    highestDate = priceEstimate.Date;
                }

                if (lowest == null || price < lowest)
                {
                    lowest = price;
                    lowestDate = priceEstimate.Date;
                }

                sum = sum == null ? price : sum + price;
                ++count;
            }

            if (sum == null) return null;

            return new Report
            {
                AveragePrice = sum.Value / count,
                HighestPriceDate = highestDate,
                LowestPriceDate = lowestDate,
                HighestPrice = highest.Value,
                LowestPrice = lowest.Value,
            };
        }

        public Report Execute(Guid analysisId)
        {
            Analysis analysis = _repository.Get(analysisId);
            if (analysis == null) return null;
            return Generate(analysis);
        }

        public GenerateReport(IAnalysisRepository repository)
        {
            _repository = repository;
        }
    }
}
