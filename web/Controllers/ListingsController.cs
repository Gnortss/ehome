using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace web.Controllers
{
    public class ListingsController : Controller
    {
        private readonly EhomeContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public ListingsController(EhomeContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: Listings
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            var ehomeContext = _context.Listings
                .Include(l => l.Region)
                .Include(l => l.LType)
                .Include(l => l.REGroup)
                .Include(l => l.REGroup.REType)
                .Where(l => l.Owner == currentUser);
                
            return View(await ehomeContext.ToListAsync());
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings
                .Include(l => l.Region)
                .Include(l => l.LType)
                .Include(l => l.REGroup)
                .Include(l => l.REGroup.REType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // GET: Listings/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ListingType"] = new SelectList(_context.ListingType, "Id", "Id");
            ViewData["GroupId"] = new SelectList(_context.RealEstateGroup, "Id", "Id");
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "Id");
            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateOfEntry,RegionId,Address,Size,Year,ImageLink,Description,Price,GroupId,ListingType")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListingType"] = new SelectList(_context.ListingType, "Id", "Type", listing.ListingType);
            ViewData["GroupId"] = new SelectList(_context.RealEstateGroup, "Id", "Id", listing.GroupId);
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "Id", listing.RegionId);
            return View(listing);
        }

        // GET: Listings/Edit/5
        // [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {

            var currentUser = await _usermanager.GetUserAsync(User);
            ViewData["isLogged"] = currentUser != null;

            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings.FindAsync(id);
            var group = await _context.RealEstateGroup.FindAsync(listing.GroupId);
            if (listing == null)
            {
                return NotFound();
            }
            ViewData["ListingType"] = new SelectList(_context.ListingType, "Id", "Type", listing.ListingType);
            ViewData["Group"] = new SelectList(_context.RealEstateGroup, "Id", "Group", group.TypeId);
            ViewData["Region"] = new SelectList(_context.Region, "Id", "Name", listing.RegionId);
            return View(listing);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfEntry,RegionId,Address,Size,Year,ImageLink,Description,Price,GroupId,ListingType")] Listing listing)
        {
            if (id != listing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingExists(listing.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListingType"] = new SelectList(_context.ListingType, "Id", "Id", listing.ListingType);
            ViewData["GroupId"] = new SelectList(_context.RealEstateGroup, "Id", "Id", listing.GroupId);
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "Id", listing.RegionId);
            return View(listing);
        }

        // GET: Listings/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings
                .Include(l => l.LType)
                .Include(l => l.REGroup)
                .Include(l => l.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listing = await _context.Listings.FindAsync(id);
            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListingExists(int id)
        {
            return _context.Listings.Any(e => e.Id == id);
        }
    }
}
