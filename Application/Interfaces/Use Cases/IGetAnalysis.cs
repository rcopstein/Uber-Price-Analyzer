using System;
using Application.DTOs;

namespace Application.Interfaces.UseCases
{
    public interface IGetAnalysis
    {
        AnalysisDTO Execute(Guid id);
    }
}
