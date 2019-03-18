using Application.Interfaces.UseCases;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using System.Net.Http;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AnalysisController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIncludeAnalysis _includeAnalysis;
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
        public ActionResult<AnalysisDTO> Get(Guid id)
        {
            AnalysisDTO result = _getAnalysis.Execute(id);
            return Json(result);
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
            IHttpClientFactory httpClientFactory,
            IIncludeAnalysis includeAnalysis,
            IGetAllAnalyses getAllAnalyses,
            IGetAnalysis getAnalysis)
        {
            _httpClientFactory = httpClientFactory;
            _includeAnalysis = includeAnalysis;
            _getAllAnalyses = getAllAnalyses;
            _getAnalysis = getAnalysis;
        }
    }
}
