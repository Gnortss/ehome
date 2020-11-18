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
        public int RegionId { get; set; }
        [ForeignKey("RegionId")]
        public Region Region { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }
        public int Year { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public RealEstateGroup REGroup { get; set; }

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