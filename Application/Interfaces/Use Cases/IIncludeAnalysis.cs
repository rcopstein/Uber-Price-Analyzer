using Application.DTOs;

namespace Application.Interfaces.UseCases
{
    public interface IIncludeAnalysis
    {
        long Execute(AnalysisIncludeDTO command);
    }
}
