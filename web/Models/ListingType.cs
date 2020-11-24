using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class ListingType
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Tip ponudbe")]
        public string Type { get; set; }
    }
}