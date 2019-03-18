﻿using System;
using Application.DTOs;

namespace Application.Interfaces.UseCases
{
    public interface IIncludeAnalysis
    {
        Guid Execute(AnalysisIncludeDTO command);
    }
}
