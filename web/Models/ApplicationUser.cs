using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ImageLink { get; set; }

        public List<Listing> Listings { get; set; }
    }
}