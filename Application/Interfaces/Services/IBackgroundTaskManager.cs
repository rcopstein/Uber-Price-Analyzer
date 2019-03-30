using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IBackgroundTaskManager
    {
        void InitializeTaskForAnalysis(Analysis analysis);
    }
}
