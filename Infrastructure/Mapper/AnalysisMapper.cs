using System.Collections.Generic;
using Infrastructure.DTOs;
using Domain.Models;
using System;
using System.Text;

namespace Infrastructure.Mapper
{
    public static class AnalysisMapper
    {
        public static Analysis FromDTO(AnalysisDTO dto)
        {
            if (dto == null) return null;

            var days = dto.Weekdays.Split(',');
            var weekdays = new DayOfWeek[days.Length];

            for (int i = 0; i < weekdays.Length; ++i)
                weekdays[i] = (DayOfWeek) Convert.ToInt32(days[i]);

            return new Analysis()
            {
                Id = dto.Id,
                Status = dto.Status,
                EndLocation = LocationMapper.FromDTO(dto.EndLocation),
                StartLocation = LocationMapper.FromDTO(dto.StartLocation),
                TimeFrame = new TimeFrame()
                {
                    DateTo = dto.DateTo,
                    DateFrom = dto.DateFrom,
                    TimeTo = dto.TimeTo,
                    TimeFrom = dto.TimeFrom,
                    Every = dto.Every,
                    Weekdays = weekdays
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

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < entity.TimeFrame.Weekdays.Length; ++i)
            {
                if (i > 0) builder.Append(',');
                builder.Append((int)entity.TimeFrame.Weekdays[i]);
            }

            return new AnalysisDTO()
            {
                Id = entity.Id,
                Status = entity.Status,
                Weekdays = builder.ToString(),
                Every = entity.TimeFrame.Every,
                DateTo = entity.TimeFrame.DateTo,
                TimeTo = entity.TimeFrame.TimeTo,
                DateFrom = entity.TimeFrame.DateFrom,
                TimeFrom = entity.TimeFrame.TimeFrom,
                Prices = PriceEstimateMapper.ToDTO(entity.Prices),
                EndLocation = LocationMapper.ToDTO(entity.EndLocation),
                StartLocation = LocationMapper.ToDTO(entity.StartLocation),
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
