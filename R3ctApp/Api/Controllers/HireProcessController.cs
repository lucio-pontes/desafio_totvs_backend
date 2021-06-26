using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R3ctApp.Api.Models;
using R3ctApp.Api.Responses;
using R3ctApp.DataAccess;

namespace R3ctApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HireProcessController : ControllerBase
    {
        private readonly ApplicationDbContext _pgcontext;

        public HireProcessController(ApplicationDbContext context)
        {
            _pgcontext = context;
        }

        // GET: api/hireprocess/hirejobs/{id}
        [HttpGet("hirejobs/{id}")]
        public async Task<ActionResult<IEnumerable<HireProcessResp>>> GetHireProcessByHireJobId(long id)
        {
            return await _pgcontext.HireProcess
                .Include(p => p.HireCandidate)
                .Include(p => p.HireJob)
                .Where(p => p.HireJobId == id)
                .Select(p => new HireProcessResp {
                    Id = p.Id,
                    Job = p.HireJob.Description,
                    Name = p.HireCandidate.Name,
                    Phone = p.HireCandidate.Phone,
                    Email = p.HireCandidate.Email,
                    Status = p.Status
                })
                .ToListAsync();
        }

        // GET: api/hireprocess/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HireProcessResp>> GetHireProcess(long id)
        {
            var hireProcess = await _pgcontext.HireProcess
                .Where(p => p.Id == id)
                .Select(p => new HireProcessResp {
                    Id = p.Id,
                    Job = p.HireJob.Description,
                    Name = p.HireCandidate.Name,
                    Phone = p.HireCandidate.Phone,
                    Email = p.HireCandidate.Email,
                    Status = p.Status
                })
                .FirstAsync();

            if (hireProcess == null)
            {
                return NotFound();
            }

            return hireProcess;
        }

        // POST: api/hireprocess
        public async Task<IActionResult> PostHireProcess(HireProcess hireProcess)
        {
            _pgcontext.HireProcess.Add(hireProcess);
            await _pgcontext.SaveChangesAsync();

            hireProcess.HireJob = await _pgcontext.HireJobs.FindAsync(hireProcess.HireJobId);
            hireProcess.HireCandidate = await _pgcontext.HireCandidates.FindAsync(hireProcess.HireCandidateId);

            var hireProcessResp = new HireProcessResp
            {
                Id = hireProcess.Id,
                Job = hireProcess.HireJob.Description,
                Name = hireProcess.HireCandidate.Name,
                Phone = hireProcess.HireCandidate.Phone,
                Email = hireProcess.HireCandidate.Email,
                Status = hireProcess.Status
            };

            return CreatedAtAction(nameof(GetHireProcess), new { id = hireProcess.Id }, hireProcessResp);
        }

        // DELETE: api/hireprocess/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHireProcess(long id)
        {
            var hireProcess = await _pgcontext.HireProcess.FindAsync(id);
            if (hireProcess == null)
            {
                return NotFound();
            }

            _pgcontext.HireProcess.Remove(hireProcess);
            await _pgcontext.SaveChangesAsync();

            return NoContent();
        }

    }
}
