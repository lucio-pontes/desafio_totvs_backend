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
        
namespace R3ctApp.Api.Selecao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HireCandidatesController : ControllerBase
    {
        private readonly ApplicationDbContext _pgcontext;

        public HireCandidatesController(ApplicationDbContext context)
        {
            _pgcontext = context;
        }

        // GET: api/hirecandidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HireCandidate>>> GetHireCandidates()
        {
            return await _pgcontext.HireCandidates.ToListAsync();
        }

        // GET: api/hirecandidates/news
        [HttpGet("news")]
        public async Task<ActionResult<IEnumerable<CandidateSelectResp>>> GetHireCandidatesNews()
        {
            List<long> ids = await _pgcontext.HireProcess
                .Where(p => !p.Status.Equals("Canceled"))
                .Select(p => p.Id)
                .ToListAsync();

            return await _pgcontext.HireCandidates
                .Where(c => !ids.Contains(c.Id))
                .Select(c => new CandidateSelectResp
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        // GET: api/hirecandidates/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HireCandidate>> GetHireCandidate(long id)
        {
            var hireCandidate = await _pgcontext.HireCandidates.FindAsync(id);

            if (hireCandidate == null)
            {
                return NotFound();
            }

            return hireCandidate;
        }

        // PUT: api/hirecandidates/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHireCandidate(long id, HireCandidate hireCandidate)
        {
            if (id != hireCandidate.Id)
            {
                return BadRequest();
            }

            _pgcontext.Entry(hireCandidate).State = EntityState.Modified;

            try
            {
                await _pgcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsHireCandidateExists(id))
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

        // POST: api/hirecandidates
        [HttpPost]
        public async Task<ActionResult<HireCandidate>> PostHireCandidate(HireCandidate hireCandidate)
        {
            _pgcontext.HireCandidates.Add(hireCandidate);
            await _pgcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHireCandidate), new { id = hireCandidate.Id }, hireCandidate);
        }

        // DELETE: api/hirecandidates/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHireCandidate(long id)
        {
            var hireCandidate = await _pgcontext.HireCandidates.FindAsync(id);
            if (hireCandidate == null)
            {
                return NotFound();
            }

            _pgcontext.HireCandidates.Remove(hireCandidate);
            await _pgcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool IsHireCandidateExists(long id)
        {
            return _pgcontext.HireCandidates.Any(e => e.Id == id);
        }

        // GET: api/hirecandidates/{id}/hirecurriculum
        [HttpGet("{id}/hirecurriculum")]
        public async Task<ActionResult<HireCurriculum>> GetHireCurriculumWithHireCandidateId(long id)
        {
            var hireCurriculum= await _pgcontext.HireCurriculums
                .Where(c => c.HireCandidateId == id)
                .FirstAsync();

            if (hireCurriculum == null)
            {
                return NotFound();
            }

            return hireCurriculum;
        }

    }
}
