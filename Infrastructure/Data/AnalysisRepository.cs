using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Infrastructure.Mapper;
using Infrastructure.DTOs;
using Domain.Models;
using System.Linq;

namespace Infrastructure.Data
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly DbContext _context;

        public IEnumerable<Analysis> GetAll()
        {
            var result = _context
                .Set<AnalysisDTO>()
                .Include(x => x.StartLocation)
                .Include(x => x.EndLocation)
                .ToList();
            return AnalysisMapper.FromDTO(result);
        }

        public void Add(Analysis analysis)
        {
            var dto = AnalysisMapper.ToDTO(analysis);
            _context.Set<AnalysisDTO>().Add(dto);
            _context.SaveChanges();
        }

        public AnalysisRepository(DbContext context)
        {
            _context = context;
        }
    }
}
