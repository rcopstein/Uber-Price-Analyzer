﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Infrastructure.Mapper;
using Infrastructure.DTOs;
using Domain.Repositories;
using Domain.Models;
using System.Linq;
using System;

namespace Infrastructure.Data.Repository
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly DbContext _context;

        public IEnumerable<Analysis> List()
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

            analysis.Id = dto.Id;
        }

        public Analysis Get(Guid id)
        {
            var dto = _context.Set<AnalysisDTO>()
                .Include(x => x.StartLocation)
                .Include(x => x.EndLocation)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();

            return AnalysisMapper.FromDTO(dto);
        }

        public AnalysisRepository(DbContext context)
        {
            _context = context;
        }
    }
}