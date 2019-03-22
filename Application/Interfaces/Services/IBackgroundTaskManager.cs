using System;

namespace Application.Interfaces.Services
{
    public interface IBackgroundTaskManager
    {
        void InitializeTaskForAnalysis(Guid id);
    }
}
