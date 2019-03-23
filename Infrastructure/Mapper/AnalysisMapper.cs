using System.Collections.Generic;
using Infrastructure.DTOs;
using Domain.Models;

namespace Infrastructure.Mapper
{
    public static class AnalysisMapper
    {
        public static Analysis FromDTO(AnalysisDTO dto)
        {
            if (dto == null) return null;

            return new Analysis()
            {
                Id = dto.Id,
                Status = dto.Status,
                EndLocation = LocationMapper.FromDTO(dto.EndLocation),
                StartLocation = LocationMapper.FromDTO(dto.StartLocation),
                TimeFrame = new TimeFrame()
                {
                    To = dto.To,
                    From = dto.From,
                    Every = dto.Every
                },
                Prices = PriceEstimateMapper.FromDTO(dto.Prices)
            };
        }

        public static IEnumerable<Analysis> FromDTO(
            IEnumerable<AnalysisDTO> dtos)
        {
            List<Analysis> result = new List<Analysis>();
            foreach (var dto in dtos)
                result.Add(FromDTO(dto));

            return result;
        }

        public static AnalysisDTO ToDTO(Analysis entity)
        {
            if (entity == null) return null;

            return new AnalysisDTO()
            {
                Id = entity.Id,
                Status = entity.Status,
                EndLocation = LocationMapper.ToDTO(entity.EndLocation),
                StartLocation = LocationMapper.ToDTO(entity.StartLocation),
                To = entity.TimeFrame.To,
                From = entity.TimeFrame.From,
                Every = entity.TimeFrame.Every,
                Prices = PriceEstimateMapper.ToDTO(entity.Prices)
            };
        }

        public static IEnumerable<AnalysisDTO> ToDTO(
            IEnumerable<Analysis> entities)
        {
            List<AnalysisDTO> result = new List<AnalysisDTO>();
            foreach (var entity in entities)
                result.Add(ToDTO(entity));

            return result;
        }
    }
}
