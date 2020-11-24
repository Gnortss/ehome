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
        [Route("Listings/Details/{id?}/{userid?}")]
        public async Task<IActionResult> Details(int? id, string userid)
        {
            ViewData["UserId"] = userid;

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
            ViewData["ListingType"] = new SelectList(_context.ListingType, "Id", "Type");
            ViewData["GroupId"] = new SelectList(_context.RealEstateGroup, "Id", "FullName");
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "Name");
            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("RegionId,Address,Size,Year,ImageLink,Description,Price,GroupId,ListingType")] Listing listing)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                if(listing.ImageLink == "" || listing.ImageLink == null)
                    listing.ImageLink = this.getRandomImage();
                listing.DateOfEntry = DateTime.Now;
                listing.Owner = currentUser;
                _context.Add(listing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListingType"] = new SelectList(_context.ListingType, "Id", "Type", listing.ListingType);
            ViewData["GroupId"] = new SelectList(_context.RealEstateGroup, "Id", "FullName", listing.GroupId);
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "Name", listing.RegionId);
            return View(listing);
        }

        // GET: Listings/Edit/5/{userid?}
        // [Authorize]
        [Route("Listings/Edit/{id?}/{userid?}")]
        public async Task<IActionResult> Edit(int? id, string userid)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            ViewData["isLogged"] = currentUser != null;
            ViewData["UserId"] = userid;
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
            ViewData["FullGroup"] = new SelectList(_context.RealEstateGroup.Where(e => e.Group == group.Group), "Id", "FullName", group.TypeId);
            ViewData["Region"] = new SelectList(_context.Region, "Id", "Name", listing.RegionId);
            return View(listing);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Listings/Edit/{id?}/{userid?}")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfEntry,RegionId,Address,Size,Year,ImageLink,Description,Price,GroupId,ListingType")] Listing listing, string userid)
        {
            if (id != listing.Id)
            {
                return NotFound();
            }
            var l = await _context.Listings.FindAsync(id);
            var group = await _context.RealEstateGroup.FindAsync(l.GroupId);
            ViewData["ListingType"] = new SelectList(_context.ListingType, "Id", "Type", l.ListingType);
            ViewData["FullGroup"] = new SelectList(_context.RealEstateGroup.Where(e => e.Group == group.Group), "Id", "FullName", group.TypeId);
            ViewData["Region"] = new SelectList(_context.Region, "Id", "Name", l.RegionId);

            if (ModelState.IsValid)
            {
                try
                {
                    if(listing.ImageLink == "" || listing.ImageLink == null)
                        listing.ImageLink = this.getRandomImage();
                    _context.DetachLocal(listing, id);
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
                if(userid != null)
                    return RedirectToAction("Details", "Admin", new {id=userid});

                return RedirectToAction(nameof(Index));
            }
            return View(listing);
        }

        // GET: Listings/Delete/5
        [Route("Listings/Delete/{id?}/{userid?}")]
        [Authorize]
        public async Task<IActionResult> Delete(int? id, string userid)
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
        [Route("Listings/Delete/{id}/{userid?}")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id, string userid)
        {
            var listing = await _context.Listings.FindAsync(id);
            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();
            
            if(userid != null)
                return RedirectToAction("Details", "Admin", new {id=userid});
            return RedirectToAction(nameof(Index));
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
