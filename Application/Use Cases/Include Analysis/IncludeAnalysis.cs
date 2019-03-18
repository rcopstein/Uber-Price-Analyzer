using System;
using Application.DTOs;
using Application.Validation;
using Application.Interfaces.UseCases;
using Application.Interfaces.Repositories;
using Domain.Models;
using Application.Exceptions;

namespace Application.UseCases.IncludeAnalysis
{
    public class IncludeAnalysis : IIncludeAnalysis
    {
        private readonly IAnalysisRepository _repository;

        public Guid Execute(AnalysisIncludeDTO command)
        {
            Validator validator = new Validator();
            ValidationSummary summary = validator.Validate(command);
            if (!summary.IsValid) throw new ValidationException(summary);

            Analysis analysis = new Analysis
            {
                StartLocation = command.StartLocation,
                EndLocation = command.EndLocation,
                TimeFrame = command.TimeFrame
            };
            _repository.Add(analysis);

            return analysis.Id;
        }

        public IncludeAnalysis(IAnalysisRepository repository)
        {
            _repository = repository;
        }
    }
}
