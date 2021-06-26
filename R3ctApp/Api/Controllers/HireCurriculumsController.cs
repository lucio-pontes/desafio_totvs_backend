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
    public class HireCurriculumsController : ControllerBase
    {
        private readonly ApplicationDbContext _pgcontext;

        public HireCurriculumsController(ApplicationDbContext context)
        {
            _pgcontext = context;
        }

        // GET: api/hirecurriculums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HireCurriculumResp>>> GetHireCurriculums()
        {
            return await _pgcontext.HireCurriculums
                .Select(c => new HireCurriculumResp
                {
                    Id = c.Id,
                    Name = c.HireCandidate.Name,
                    WorksHistory = c.WorksHistory,
                    AcademicsHistory = c.AcademicsHistory,
                    Courses = c.Courses,
                    Summary = c.Summary
                })
                .ToListAsync();
        }

        // GET: api/hirecurriculums/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HireCurriculumResp>> GetHireCurriculum(long id)
        {
            var hireCurriculum = await _pgcontext.HireCurriculums
                .Include(c => c.HireCandidate)
                .Where(c => c.Id == id)
                .Select(c => new HireCurriculumResp {
                    Id = c.Id,
                    Name = c.HireCandidate.Name,
                    WorksHistory = c.WorksHistory,
                    AcademicsHistory = c.AcademicsHistory,
                    Courses = c.Courses,
                    Summary = c.Summary
                })
                .FirstAsync();

            if (hireCurriculum == null)
            {
                return NotFound();
            }

            return hireCurriculum;
        }

        // PUT: api/hirecurriculums/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHireCurriculum(long id, HireCurriculum hireCurriculum)
        {
            if (id != hireCurriculum.Id)
            {
                return BadRequest();
            }

            _pgcontext.Entry(hireCurriculum).State = EntityState.Modified;

            try
            {
                await _pgcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsHireCurriculumExists(id))
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

        // POST: api/hirecurriculums
        [HttpPost]
        public async Task<ActionResult<HireCurriculumResp>> PostHireCurriculum(HireCurriculum hireCurriculum)
        {
            _pgcontext.HireCurriculums.Add(hireCurriculum);
            await _pgcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHireCurriculum), new { id = hireCurriculum.Id }, hireCurriculum);
        }

        // DELETE: api/hirecurriculums/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHireCurriculum(long id)
        {
            var hireCurriculum = await _pgcontext.HireCurriculums.FindAsync(id);
            if (hireCurriculum == null)
            {
                return NotFound();
            }

            _pgcontext.HireCurriculums.Remove(hireCurriculum);
            await _pgcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool IsHireCurriculumExists(long id)
        {
            return _pgcontext.HireCurriculums.Any(e => e.Id == id);
        }

    }
}
