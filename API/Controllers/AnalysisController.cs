using Application.Interfaces.UseCases;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using System.Net.Http;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AnalysisController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIncludeAnalysis _includeAnalysis;
        private readonly IGetAllAnalyses _getAllAnalyses;

        public AnalysisController(
            IHttpClientFactory httpClientFactory,
            IIncludeAnalysis includeAnalysis,
            IGetAllAnalyses getAllAnalyses)
        {
            _httpClientFactory = httpClientFactory;
            _includeAnalysis = includeAnalysis;
            _getAllAnalyses = getAllAnalyses;
        }

        // Location start = new Location(37.7752315f, -122.418075f);
        // Location end = new Location(37.7752415f, -122.518075f);

        // GET: api/analysis
        [HttpGet]
        public ActionResult<IEnumerable<AnalysisDisplayDTO>> GetAll()
        {
            IEnumerable<AnalysisDisplayDTO> analyses =
                _getAllAnalyses.Execute();

            return Json(analyses);
        }

        // POST: api/analysis
        [HttpPost]
        public IActionResult Post([FromBody] AnalysisIncludeDTO command)
        {
            long id = _includeAnalysis.Execute(command);
            return CreatedAtAction(nameof(GetAll), id); // TODO Change to 'Get'
        }

    }
}
