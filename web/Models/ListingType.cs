using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class ListingType
    {
        [Key]
        public string Type { get; set; }
    }
}