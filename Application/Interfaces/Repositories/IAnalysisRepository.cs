using System.Collections.Generic;
using Domain.Models;
using System;

namespace Application.Interfaces.Repositories
{
    public interface IAnalysisRepository
    {
        IEnumerable<Analysis> GetAll();

        void Add(Analysis analysis);

        Analysis Get(Guid id);
    }
}
