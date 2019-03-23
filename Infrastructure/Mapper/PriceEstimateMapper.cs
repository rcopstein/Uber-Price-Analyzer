using System.Collections.Generic;
using Domain.Models;
using Infrastructure.DTOs;

namespace Infrastructure.Mapper
{
    public static class PriceEstimateMapper
    {
        public static PriceEstimate FromDTO(PriceEstimateDTO dto)
        {
            if (dto == null) return null;

            return new PriceEstimate()
            {
                Date = dto.Date,
                ProductId = dto.ProductId,
                LowEstimate = dto.LowEstimate,
                HighEstimate = dto.HighEstimate
            };
        }

        public static IEnumerable<PriceEstimate> FromDTO(
            IEnumerable<PriceEstimateDTO> dtos)
        {
            List<PriceEstimate> result = new List<PriceEstimate>();
            foreach (var dto in dtos) result.Add(FromDTO(dto));
            return result;
        }

        public static PriceEstimateDTO ToDTO(PriceEstimate entity)
        {
            if (entity == null) return null;

            return new PriceEstimateDTO()
            {
                Date = entity.Date,
                ProductId = entity.ProductId,
                LowEstimate = entity.LowEstimate,
                HighEstimate = entity.HighEstimate
            };
        }

        public static IEnumerable<PriceEstimateDTO> ToDTO(
            IEnumerable<PriceEstimate> entities)
        {
            List<PriceEstimateDTO> result = new List<PriceEstimateDTO>();
            foreach (var entity in entities) result.Add(ToDTO(entity));
            return result;
        }
    }
}
