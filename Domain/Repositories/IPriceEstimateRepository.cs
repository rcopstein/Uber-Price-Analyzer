using Domain.Models;

namespace Domain.Repositories
{
    public interface IPriceEstimateRepository
    {
        void Add(PriceEstimate estimate);
    }
}
