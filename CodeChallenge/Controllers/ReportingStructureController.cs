using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingStructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        [HttpGet("{id}", Name = "getReportingStructureByEmployeeId")]
        public IActionResult getReportingStructureByEmployeeId(String id)
        {
            _logger.LogDebug($"Received Reporting Structure GET request for '{id}'");

            var employeeDirectReport = _reportingStructureService.GetReportingStructureByEmployeeId(id);

            if (employeeDirectReport == null)
                return NotFound();

            return Ok(employeeDirectReport);
        }
    }
}
