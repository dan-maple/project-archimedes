using AutoMapper;
using CoreApi.Dtos;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ApiDbContext               _context;
        private readonly IMapper                    _mapper;
        private readonly ILogger<JobsController>    _logger;

        public JobsController
        (
            ApiDbContext            context,
            IMapper                 mapper,
            ILogger<JobsController> logger
        )
        {
            _context = context;
            _mapper  = mapper;
            _logger  = logger;
        }

        /// <summary>
        /// Gets all Jobs.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Jobs
        /// 
        /// </remarks>
        /// <returns>All Jobs.</returns>
        /// <response code="200">Returns all jobs.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetJobs()
        {
            List<Job>    jobs    = await _context.Jobs.ToListAsync();
            List<JobDto> jobDtos = _mapper.Map<List<Job>, List<JobDto>>(jobs);
            _logger.LogInformation($"Retrieved {jobs.Count} jobs.");

            return jobDtos;
        }

        /// <summary>
        /// Gets a Job.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Jobs/5
        /// 
        /// </remarks>
        /// <returns>A Job.</returns>
        /// <response code="200">Returns the job.</response>
        /// <response code="404">If the job is not found.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JobDto>> GetJob(long id)
        {
            Job job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                _logger.LogInformation($"Job '{id}' not found.");
                return NotFound();
            }

            JobDto jobDto = _mapper.Map<JobDto>(job);

            return jobDto;
        }

        /// <summary>
        /// Updates a job.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/Jobs/5
        ///     {
        ///         "id": 5,
        ///         "description": "Job Description",
        ///         "status": 1
        ///     }
        /// 
        /// </remarks>
        /// <response code="204">Returns nothing.</response>
        /// <response code="400">If ID and job ID do not match.</response>
        /// <response code="404">If the job is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutJob(long id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!JobExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a job.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Jobs
        ///     {
        ///         "id": 1,
        ///         "description": "Job Description"
        ///     }
        /// 
        /// </remarks>
        /// <returns>A newly created Job.</returns>
        /// <response code="201">Returns the newly created job.</response>
        /// <response code="400">If the job is null.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JobDto>> PostJob(Job job)
        {
            // Prevent certain fields being set.
            job.Id      = 0;
            job.Status  = JobStatus.Created;

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            JobDto jobDto = _mapper.Map<JobDto>(job);
            
            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, jobDto);
        }

        /// <summary>
        /// Deletes a Job.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Jobs/5
        /// 
        /// </remarks>
        /// <returns>A Job.</returns>
        /// <response code="200">Returns the deleted job.</response>
        /// <response code="404">If the job is not found.</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Job>> DeleteJob(long id)
        {
            Job job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return job;
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
