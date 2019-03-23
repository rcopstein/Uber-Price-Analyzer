using System;
using Domain.Models;

namespace Application.Interfaces.UseCases
{
    public interface IGetAnalysis
    {
        Analysis Execute(Guid id);
    }
}
