using CoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ApiDbContext           _context;
        private readonly ILogger<JobController> _logger;

        public JobController
        (
            ApiDbContext           context,
            ILogger<JobController> logger
        )
        {
            _context = context;
            _logger  = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Job>> Get()
        {
            List<Job> jobs = _context.Jobs.ToList();
            _logger.LogInformation($"Retrieved {jobs.Count} jobs.");
            return jobs;
        }
    }
}
