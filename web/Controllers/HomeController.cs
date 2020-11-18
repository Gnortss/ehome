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

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly EhomeContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, EhomeContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> IndexAsync(string vrstaNepremicnine,string vrstaPonudbe, string regija, string velikost,string leto,string cena)
        {
            if(vrstaNepremicnine != null) {
                string[] loceno = velikost.Split(",");
                int prva = Int32.Parse(loceno[0]);
                int druga = Int32.Parse(loceno[1]);
                var ehomeContext = _context.Listings
                    .Include(l => l.LType)
                    .Include(l => l.REGroup)
                    .Include(l => l.Region)
                    // .Where(l => l.RealEstateType == vrstaNepremicnine && l.ListingType == vrstaPonudbe && l.Size >= prva && l.Size <= druga)
                    .OrderByDescending(l => l.DateOfEntry);
                return View(await ehomeContext.ToListAsync());
            }
            else {
                var ehomeContext = _context.Listings
                    .Include(l => l.LType)
                    .Include(l => l.REGroup)
                    .Include(l => l.Region)
                    .OrderByDescending(l => l.DateOfEntry);
                    return View(await ehomeContext.ToListAsync());
            }

            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
