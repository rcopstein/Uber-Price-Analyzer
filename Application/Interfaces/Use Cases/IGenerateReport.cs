using System;
using Domain.Models;

namespace Application.Interfaces.UseCases
{
    public interface IGenerateReport
    {
        Report Execute(Guid analysisId);
    }
}
