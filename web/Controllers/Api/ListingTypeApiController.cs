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
    [Route("api/v1/ListingType")]
    [ApiController]
    public class ListingTypeApiController : ControllerBase
    {
        private readonly EhomeContext _context;

        public ListingTypeApiController(EhomeContext context)
        {
            _context = context;
        }

        // GET: api/ListingTypeApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListingType>>> GetListingType()
        {
            return await _context.ListingType.ToListAsync();
        }

        // GET: api/ListingTypeApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListingType>> GetListingType(int id)
        {
            var listingType = await _context.ListingType.FindAsync(id);

            if (listingType == null)
            {
                return NotFound();
            }

            return listingType;
        }

        // PUT: api/ListingTypeApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListingType(int id, ListingType listingType)
        {
            if (id != listingType.Id)
            {
                return BadRequest();
            }

            _context.Entry(listingType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingTypeExists(id))
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

        // POST: api/ListingTypeApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ListingType>> PostListingType(ListingType listingType)
        {
            _context.ListingType.Add(listingType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ListingTypeExists(listingType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetListingType", new { id = listingType.Id }, listingType);
        }

        // DELETE: api/ListingTypeApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ListingType>> DeleteListingType(int id)
        {
            var listingType = await _context.ListingType.FindAsync(id);
            if (listingType == null)
            {
                return NotFound();
            }

            _context.ListingType.Remove(listingType);
            await _context.SaveChangesAsync();

            return listingType;
        }

        private bool ListingTypeExists(int id)
        {
            return _context.ListingType.Any(e => e.Id == id);
        }
    }
}
