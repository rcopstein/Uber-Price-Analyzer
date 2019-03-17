using System.Collections.Generic;
using Domain.Models;

namespace Application.Interfaces.Repositories
{
    public interface IAnalysisRepository
    {
        IEnumerable<Analysis> GetAll();

        void Add(Analysis analysis);
    }
}
