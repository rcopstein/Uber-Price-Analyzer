using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IUberAPI
    {
        Task<IEnumerable<PriceEstimate>> Estimate(Location start, Location end);
    }
}
