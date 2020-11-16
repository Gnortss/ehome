using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Listing
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateOfEntry { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }
        public int Year { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public string RealEstateType { get; set; }
        [ForeignKey("RealEstateType")]
        public RealEstateType REType { get; set; }

        public string ListingType { get; set; }
        [ForeignKey("ListingType")]
        public ListingType LType { get; set;}
        public ApplicationUser Owner { get; set; }

    }
}