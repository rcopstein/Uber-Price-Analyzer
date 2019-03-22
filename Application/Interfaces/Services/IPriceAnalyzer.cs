using System.Threading.Tasks;
using System;

namespace Application.Interfaces.Services
{
    public interface IPriceAnalyzer
    {
        Task StartAnalysis(Guid id);
    }
}
