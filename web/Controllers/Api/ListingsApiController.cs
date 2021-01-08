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
using Microsoft.AspNetCore.Identity;

namespace web.Controllers_Api
{
    [Route("api/v1/Listing")]
    [ApiController]
    [ApiKeyAuth]
    public class ListingsApiController : ControllerBase
    {
        private readonly EhomeContext _context;

        private readonly UserManager<ApplicationUser> _usermanager;

        public ListingsApiController(EhomeContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }

        // GET: api/ListingsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Listing>>> GetListings()
        {
            return await _context.Listings
                .Include(l => l.Region)
                .Include(l => l.REGroup)
                .Include(l => l.LType)
                .OrderByDescending(l => l.DateOfEntry)
                .ToListAsync();
        }

        // GET: api/ListingsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(int id)
        {
            var listing = await _context.Listings
                .Include(l => l.Region)
                .Include(l => l.REGroup)
                .Include(l => l.LType)
                .FirstAsync(l => l.Id == id);

            if (listing == null)
            {
                return NotFound();
            }

            return listing;
        }

        // PUT: api/ListingsApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(int id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }

            _context.Entry(listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
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

        // POST: api/ListingsApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Listing>> PostListing([Bind("regionId,address,size,year,imageLink,description,price,groupId,listingType,ownerId")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                if(listing.ImageLink == "" || listing.ImageLink == null)
                    listing.ImageLink = this.getRandomImage();
                listing.DateOfEntry = DateTime.Now;
                
                _context.Add(listing);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetListing", new { id = listing.Id }, listing);
            }

            return BadRequest();
        }

        // DELETE: api/ListingsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Listing>> DeleteListing(int id)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();

            return listing;
        }

        private bool ListingExists(int id)
        {
            return _context.Listings.Any(e => e.Id == id);
        }

        private string getRandomImage(){
            var rand = new Random();
            var i = rand.Next(1, 6);
            switch(i)
            {
                case 1: return "https://miro.medium.com/max/10816/1*eO3CIaFBe7LMRePApDwSYA.jpeg";
                case 2: return "https://cache.100kvadratov.si/image/project/58/large_ljn/1/accamera1__5d2f222a8d257.jpg";
                case 3: return "https://img.nepremicnine.link/slonep_oglasi2/7520688.jpg";
                case 4: return "https://www.kras-nepremicnine.si/site/assets/files/1393/image1.jpeg";
                case 5: return "https://cache.100kvadratov.si/image/item/83/site/list_portal/4/img-4bd56aaebaf4bcfca80a2b79b368f5dc-v__5ebbde75109ae.jpg";
            }
            return "https://cache.100kvadratov.si/image/project/58/large_ljn/2/accamera5__5d2f223190d63.jpg";
        }
    }
}
