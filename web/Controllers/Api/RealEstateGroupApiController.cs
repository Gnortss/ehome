using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Filters;

namespace web.Controllers_Api
{
    [Route("api/v1/RealEstateGroup")]
    [ApiController]
    [ApiKeyAuth]
    public class RealEstateGroupApiController : ControllerBase
    {
        private readonly EhomeContext _context;

        public RealEstateGroupApiController(EhomeContext context)
        {
            _context = context;
        }

        // GET: api/RealEstateGroupApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RealEstateGroup>>> GetRealEstateGroup()
        {
            return await _context.RealEstateGroup.ToListAsync();
        }

        // GET: api/RealEstateGroupApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RealEstateGroup>> GetRealEstateGroup(int id)
        {
            var realEstateGroup = await _context.RealEstateGroup.FindAsync(id);

            if (realEstateGroup == null)
            {
                return NotFound();
            }

            return realEstateGroup;
        }

        // PUT: api/RealEstateGroupApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRealEstateGroup(int id, RealEstateGroup realEstateGroup)
        {
            if (id != realEstateGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(realEstateGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealEstateGroupExists(id))
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

        // POST: api/RealEstateGroupApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RealEstateGroup>> PostRealEstateGroup(RealEstateGroup realEstateGroup)
        {
            _context.RealEstateGroup.Add(realEstateGroup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RealEstateGroupExists(realEstateGroup.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRealEstateGroup", new { id = realEstateGroup.Id }, realEstateGroup);
        }

        // DELETE: api/RealEstateGroupApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RealEstateGroup>> DeleteRealEstateGroup(int id)
        {
            var realEstateGroup = await _context.RealEstateGroup.FindAsync(id);
            if (realEstateGroup == null)
            {
                return NotFound();
            }

            _context.RealEstateGroup.Remove(realEstateGroup);
            await _context.SaveChangesAsync();

            return realEstateGroup;
        }

        private bool RealEstateGroupExists(int id)
        {
            return _context.RealEstateGroup.Any(e => e.Id == id);
        }
    }
}
