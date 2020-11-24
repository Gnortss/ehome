using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Listing
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateOfEntry { get; set; }
        [Required]
        public int RegionId { get; set; }
        [ForeignKey("RegionId")]
        public Region Region { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        [Range(1500, 2100)]
        public int Year { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageLink { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        [Required]
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public RealEstateGroup REGroup { get; set; }
        [Required]
        public int ListingType { get; set; }
        [ForeignKey("ListingType")]
        public ListingType LType { get; set;}
        public ApplicationUser Owner { get; set; }

        public List<Favorite> Favorites { get; set; }
        public bool isFavoriteForUser(string userid) {
            if (this.Favorites == null)
            {
                return false;
            }
            return this.Favorites.Exists(e => e.UserId == userid);
        }

        public int getFavoriteId(string userid) {
            if (this.Favorites == null) return -1;
            return this.Favorites.Find(e => e.UserId == userid).Id;
        }
    }
}