using System.Collections.Generic;
using Application.DTOs;

namespace Application.Interfaces.UseCases
{
    public interface IGetAllAnalyses
    {
        IEnumerable<AnalysisDisplayDTO> Execute();
    }
}
