using System;
using Domain.Models;
using Domain.Repositories;
using Application.Interfaces.UseCases;

namespace Application.UseCases.GetAnalysis
{
    public class GetAnalysis : IGetAnalysis
    {
        private readonly IAnalysisRepository _repository;

        public Analysis Execute(Guid id) => _repository.Get(id);

        public GetAnalysis(IAnalysisRepository repository)
        {
            _repository = repository;
        }
    }
}
