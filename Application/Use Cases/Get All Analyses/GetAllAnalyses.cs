﻿using Application.Interfaces.Repositories;
using Application.Interfaces.UseCases;
using System.Collections.Generic;
using Application.DTOs;

namespace Application.UseCases.GetAllAnalyses
{
    public class GetAllAnalyses : IGetAllAnalyses
    {
        private readonly IAnalysisRepository _repository;

        public IEnumerable<AnalysisDisplayDTO> Execute()
        {
            var analyses = _repository.GetAll();
            var result = new List<AnalysisDisplayDTO>();

            foreach (var analysis in analyses)
            {
                result.Add(new AnalysisDisplayDTO
                {
                    StartLocation = analysis.StartLocation,
                    EndLocation = analysis.EndLocation,
                    TimeFrame = analysis.TimeFrame,
                    Status = analysis.Status
                });
            }

            return result;
        }

        public GetAllAnalyses(IAnalysisRepository repository)
        {
            _repository = repository;
        }
    }
}