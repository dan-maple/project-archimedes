using CoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private ApiDbContext _context;

        public JobController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Job>> Get()
        {
            List<Job> jobs = _context.Jobs.ToList();
            return jobs;
        }
    }
}
