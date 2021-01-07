using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name="Slika")]
        public string ImageLink { get; set; }

        [JsonIgnore]
        public List<Listing> Listings { get; set; }
    }
}