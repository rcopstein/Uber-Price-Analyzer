using System;
using Domain.Models;
using Application.DTOs;
using Domain.Repositories;
using Application.Interfaces.UseCases;

namespace Application.UseCases.GetAnalysis
{
    public class GetAnalysis : IGetAnalysis
    {
        private readonly IAnalysisRepository _repository;

        public AnalysisDTO Execute(Guid id)
        {
            Analysis analysis = _repository.Get(id);

            AnalysisDTO result = new AnalysisDTO()
            {
                Id = analysis.Id,
                Status = analysis.Status,
                TimeFrame = analysis.TimeFrame,
                EndLocation = analysis.EndLocation,
                StartLocation = analysis.StartLocation
            };

            return result;
        }

        public GetAnalysis(IAnalysisRepository repository)
        {
            _repository = repository;
        }
    }
}
