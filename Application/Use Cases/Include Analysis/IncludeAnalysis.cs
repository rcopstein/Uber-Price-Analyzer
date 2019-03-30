using Application.Interfaces.UseCases;
using Application.Interfaces.Services;
using Application.Exceptions;
using Domain.Repositories;
using Domain.Validation;
using Application.DTOs;
using Domain.Models;
using System;

namespace Application.UseCases.IncludeAnalysis
{
    public class IncludeAnalysis : IIncludeAnalysis
    {
        private readonly IBackgroundTaskManager _backgroundTaskManager;
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

            _backgroundTaskManager.InitializeTaskForAnalysis(analysis);

            return analysis.Id;
        }

        public IncludeAnalysis(IAnalysisRepository repository,
            IBackgroundTaskManager backgroundTaskManager)
        {
            _repository = repository;
            _backgroundTaskManager = backgroundTaskManager;
        }
    }
}
