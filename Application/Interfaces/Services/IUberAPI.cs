using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IUberAPI
    {
        Task<string> Estimate(Location start, Location end);
    }
}
