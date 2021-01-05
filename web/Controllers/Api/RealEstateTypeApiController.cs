using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers_Api
{
    [Route("api/v1/RealEstateType")]
    [ApiController]
    public class RealEstateTypeApiController : ControllerBase
    {
        private readonly EhomeContext _context;

        public RealEstateTypeApiController(EhomeContext context)
        {
            _context = context;
        }

        // GET: api/RealEstateTypeApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RealEstateType>>> GetRealEstateType()
        {
            return await _context.RealEstateType.ToListAsync();
        }

        // GET: api/RealEstateTypeApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RealEstateType>> GetRealEstateType(int id)
        {
            var realEstateType = await _context.RealEstateType.FindAsync(id);

            if (realEstateType == null)
            {
                return NotFound();
            }

            return realEstateType;
        }

        // PUT: api/RealEstateTypeApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRealEstateType(int id, RealEstateType realEstateType)
        {
            if (id != realEstateType.Id)
            {
                return BadRequest();
            }

            _context.Entry(realEstateType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealEstateTypeExists(id))
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

        // POST: api/RealEstateTypeApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RealEstateType>> PostRealEstateType(RealEstateType realEstateType)
        {
            _context.RealEstateType.Add(realEstateType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RealEstateTypeExists(realEstateType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRealEstateType", new { id = realEstateType.Id }, realEstateType);
        }

        // DELETE: api/RealEstateTypeApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RealEstateType>> DeleteRealEstateType(int id)
        {
            var realEstateType = await _context.RealEstateType.FindAsync(id);
            if (realEstateType == null)
            {
                return NotFound();
            }

            _context.RealEstateType.Remove(realEstateType);
            await _context.SaveChangesAsync();

            return realEstateType;
        }

        private bool RealEstateTypeExists(int id)
        {
            return _context.RealEstateType.Any(e => e.Id == id);
        }
    }
}
