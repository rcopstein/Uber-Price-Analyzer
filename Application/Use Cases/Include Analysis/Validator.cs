using Domain.Validation.Specifications;
using Domain.Validation;
using Application.DTOs;
using Domain.Models;

namespace Application.UseCases.IncludeAnalysis
{
    public class Validator : Validator<AnalysisIncludeDTO>
    {
        public Validator()
        {
            Add(x => x.StartLocation,
                new IsNotNull<Location>(),
                "Start Location cannot be null");

            Add(x => x.EndLocation,
                new IsNotNull<Location>(),
                "End Location cannot be null");
        }
    }
}
