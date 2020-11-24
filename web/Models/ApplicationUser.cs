using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name="Slika")]
        public string ImageLink { get; set; }

        public List<Listing> Listings { get; set; }
    }
}