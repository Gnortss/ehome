using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web.Models;
using web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly EhomeContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _usermanager;


        public HomeController(ILogger<HomeController> logger, EhomeContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _usermanager = userManager;
        }
        public async Task<IActionResult> IndexAsync(string group, int? listing, int? region, int? size, int? year, int? price)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            ViewData["isLogged"] = currentUser != null ? currentUser.Id : null;
            ViewData["Favorite"] = currentUser != null ? _context.Favorite.Where(l => l.User == currentUser).ToList() : null;
            
            if(this.NoneNull(group, listing, region, size, year, price)) {
                var (intlSize, hSize) = this.Deconstruct(size, "size");
                var (lYear, hYear) = this.Deconstruct(year, "year");
                var (lPrice, hPrice) = this.Deconstruct(price, "price");

                var ehomeContext = _context.Listings
                    .Include(l => l.LType)
                    .Include(l => l.REGroup)
                    .Include(l => l.Region)
                    .Where(l => l.REGroup.Group == group && l.ListingType == listing && l.RegionId == region
                        && (lPrice <= l.Price && l.Price <= hPrice)
                        && (lYear <= l.Year && l.Year <= hYear)
                        && (lPrice <= l.Price && l.Price <= hPrice))
                    .OrderByDescending(l => l.DateOfEntry);

                ViewData["Group"] = new SelectList(_context.RealEstateGroup.Select(e => new {e.Group}).Distinct(), "Group", "Group", group);
                ViewData["ListingType"] = new SelectList(_context.ListingType, "Id", "Type", listing);
                ViewData["Region"] = new SelectList(_context.Region, "Id", "Name", region);
                ViewData["Size"] = new SelectList(_context.SizeOptions, "Id", "DisplayName", size);
                ViewData["Year"] = new SelectList(_context.YearOptions, "Id", "DisplayName", year);
                ViewData["Price"] = new SelectList(_context.PriceOptions.Where(e => e.ListingType == listing), "Id", "DisplayName", price);

                return View(await ehomeContext.ToListAsync());
            }
            else {
                var ehomeContext = _context.Listings
                    .Include(l => l.LType)
                    .Include(l => l.REGroup)
                    .Include(l => l.Region)
                    .OrderByDescending(l => l.DateOfEntry);

                ViewData["Group"] = new SelectList(_context.RealEstateGroup.Select(e => new {e.Group}).Distinct(), "Group", "Group");
                ViewData["ListingType"] = new SelectList(_context.ListingType, "Id", "Type");
                ViewData["Region"] = new SelectList(_context.Region, "Id", "Name");
                ViewData["Size"] = new SelectList(_context.SizeOptions, "Id", "DisplayName");
                ViewData["Year"] = new SelectList(_context.YearOptions, "Id", "DisplayName");
                ViewData["Price"] = new SelectList(_context.PriceOptions.Where(e => e.ListingType == 2), "Id", "DisplayName");

                return View(await ehomeContext.ToListAsync());
            }

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ListingId")] Favorite favorite)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                favorite.User = currentUser;
                _context.Add(favorite);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favorite = await _context.Favorite.FindAsync(id);
            _context.Favorite.Remove(favorite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private (int lo, int hi) Deconstruct(int? id, string t) {
            string o = null;
            switch(t) {
                case "size":
                    o = _context.SizeOptions.Where(e => e.Id == id).First().CodeName; break;
                case "year":
                    o = _context.YearOptions.Where(e => e.Id == id).First().CodeName; break;
                case "price":
                    o = _context.PriceOptions.Where(e => e.Id == id).First().CodeName; break;
            }
            string[] p = o.Split('-');
            return (Int32.Parse(p[0]), Int32.Parse(p[1]));
        }

        private bool NoneNull(string group, int? listing, int? region, int? size, int? year, int? price)
            => group != null && listing != null && region != null && size != null && year != null && price != null;
    }
}
