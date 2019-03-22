using System.Collections.Generic;
using Domain.Models;
using System;

namespace Domain.Repositories
{
    public interface IAnalysisRepository
    {
        IEnumerable<Analysis> List();

        void Add(Analysis analysis);

        Analysis Get(Guid id);
    }
}
