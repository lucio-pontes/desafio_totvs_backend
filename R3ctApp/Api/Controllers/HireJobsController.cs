using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R3ctApp.Api.Models;
using R3ctApp.Api.Responses;
using R3ctApp.DataAccess;

namespace R3ctApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HireJobsController : ControllerBase
    {
        private readonly ApplicationDbContext _pgcontext;

        public HireJobsController(ApplicationDbContext context)
        {
            _pgcontext = context;
        }

        // GET: api/hirejobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HireJob>>> GetHireJobs()
        {
            return await _pgcontext.HireJobs.ToListAsync();
        }

        // GET: api/hirejobs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HireJob>> GetHireJob(long id)
        {
            var hireJob = await _pgcontext.HireJobs.FindAsync(id);

            if (hireJob == null)
            {
                return NotFound();
            }

            return hireJob;
        }

        // PUT: api/hirejobs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHireJob(long id, HireJob hireJob)
        {
            if (id != hireJob.Id)
            {
                return BadRequest();
            }

            _pgcontext.Entry(hireJob).State = EntityState.Modified;

            try
            {
                await _pgcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsHireJobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/hirejobs
        [HttpPost]
        public async Task<ActionResult<HireJob>> PostHireJob(HireJob hireJob)
        {
            _pgcontext.HireJobs.Add(hireJob);
            await _pgcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHireJob), new { id = hireJob.Id }, hireJob);
        }

        // DELETE: api/hirejobs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHireJobs(long id)
        {
            var hireJob = await _pgcontext.HireJobs.FindAsync(id);
            if (hireJob == null)
            {
                return NotFound();
            }

            _pgcontext.HireJobs.Remove(hireJob);
            await _pgcontext.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/hirejobs/{id}/hireprocess
        [HttpGet("{id}/hireprocess")]
        public async Task<ActionResult<IEnumerable<HireProcessResp>>> GetHireProcessWithHireJobId(long id)
        {
            return await _pgcontext.HireProcess
                .Include(p => p.HireCandidate)
                .Include(p => p.HireJob)
                .Where(p => p.HireJobId == id)
                .Select(p => new HireProcessResp
                {
                    Id = p.Id,
                    Job = p.HireJob.Description,
                    Name = p.HireCandidate.Name,
                    Phone = p.HireCandidate.Phone,
                    Email = p.HireCandidate.Email,
                    Status = p.Status
                })
                .ToListAsync();
        }

        private bool IsHireJobExists(long id)
        {
            return _pgcontext.HireJobs.Any(e => e.Id == id);
        }
    }
}
