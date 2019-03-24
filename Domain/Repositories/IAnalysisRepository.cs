using System.Collections.Generic;
using Domain.Models;
using System;

namespace Domain.Repositories
{
    public interface IAnalysisRepository
    {
        IEnumerable<Analysis> List();

        void Update(Analysis analysis);

        void Add(Analysis analysis);

        Analysis Get(Guid id);
    }
}
