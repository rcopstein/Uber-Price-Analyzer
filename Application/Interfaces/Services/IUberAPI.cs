using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IUberAPI
    {
        Task<string> EstimatePrice(Location start, Location end);
    }
}
