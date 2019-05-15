using Application.Interfaces.UseCases;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Domain.Models;
using API.Filters;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ValidationExceptionFilter]
    [ApiController]
    public class AnalysisController : Controller
    {
        private readonly IIncludeAnalysis _includeAnalysis;
        private readonly IGenerateReport _generateReport;
        private readonly IGetAllAnalyses _getAllAnalyses;
        private readonly IGetAnalysis _getAnalysis;

        // GET: api/analysis
        [HttpGet]
        public ActionResult<IEnumerable<AnalysisDisplayDTO>> GetAll()
        {
            IEnumerable<AnalysisDisplayDTO> analyses =
                _getAllAnalyses.Execute();

            return Json(analyses);
        }

        // GET: api/analysis/{id}
        [HttpGet("{id}")]
        public ActionResult<Analysis> Get(Guid id)
        {
            Analysis result = _getAnalysis.Execute(id);
            return result;
        }

        // GET: api/analysis/{id}/report
        [HttpGet("{id}/report")]
        public ActionResult<Report> GetReport(Guid id)
        {
            Report report = _generateReport.Execute(id);
            return report;
        }

        // POST: api/analysis
        [HttpPost]
        public IActionResult Post([FromBody] AnalysisIncludeDTO command)
        {
            Guid id = _includeAnalysis.Execute(command);
            var url = Url.Action(nameof(Get), new { id });
            return Created(url.ToLower(), null);
        }

        public AnalysisController(
            IIncludeAnalysis includeAnalysis,
            IGenerateReport generateReport,
            IGetAllAnalyses getAllAnalyses,
            IGetAnalysis getAnalysis)
        {
            _includeAnalysis = includeAnalysis;
            _generateReport = generateReport;
            _getAllAnalyses = getAllAnalyses;
            _getAnalysis = getAnalysis;
        }
    }
}
