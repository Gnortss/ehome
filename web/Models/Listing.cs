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
        [Display(Name="Datum vnosa")]
        public DateTime DateOfEntry { get; set; }
        [Required]
        [Display(Name="Regija")]
        public int RegionId { get; set; }
        [ForeignKey("RegionId")]
        public Region Region { get; set; }
        [Required]
        [Display(Name="Naslov")]
        public string Address { get; set; }
        [Required]
        [Display(Name="Velikost")]
        public int Size { get; set; }
        [Required, Range(1500, 2100)]
        [Display(Name="Leto")]
        public int Year { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name="Slika")]
        public string ImageLink { get; set; }
        [Required, DataType(DataType.MultilineText)]
        [Display(Name="Opis")]
        public string Description { get; set; }
        [Required, DataType(DataType.Currency)]
        [Display(Name="Cena")]
        public float Price { get; set; }
        [Required]
        [Display(Name="Vrsta - Tip")]
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public RealEstateGroup REGroup { get; set; }
        [Required]
        [Display(Name="Tip ponudbe")]
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