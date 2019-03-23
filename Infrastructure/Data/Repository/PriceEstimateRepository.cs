using Domain.Models;
using Domain.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class PriceEstimateRepository : IPriceEstimateRepository
    {
        private readonly DbContext _context;

        public void Add(PriceEstimate estimate)
        {
            var dto = PriceEstimateMapper.ToDTO(estimate);
            _context.Set<PriceEstimateDTO>().Add(dto);
            _context.SaveChanges();
        }

        public PriceEstimateRepository(DbContext context)
        {
            _context = context;
        }
    }
}
